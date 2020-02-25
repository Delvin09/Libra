using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ui.Common.Interfaces;

namespace Ui.Common
{
    public class MenuBuilder
    {
        private readonly List<Type> _registredMenu = new List<Type>();
        private readonly Menu _menu = new Menu();
        private readonly List<IMenuItem> _toAll = new List<IMenuItem>();

        public static MenuBuilder Default { get; } = new MenuBuilder();

        public MenuBuilder AddDefaultExit(bool toAll = true)
        {
            var item = new ExitMenuItem();
            if (toAll)
                _toAll.Add(item);
            _menu.AddItem(item);
            return this;
        }

        public MenuBuilder AddMenu<T>(bool toAll = false) where T : IMenu, new()
        {
            var item = new T();
            if (toAll)
                _toAll.Add(item);
            _menu.AddItem(item);
            return this;
        }

        public MenuBuilder AddMenuItem<T>(bool toAll = false) where T : IMenuItem, new()
        {
            var item = new T();
            if (toAll)
                _toAll.Add(item);
            _menu.AddItem(item);
            return this;
        }

        public MenuBuilder DetectMenuOn<T>() where T : new()
        {
            _registredMenu.Add(typeof(T));
            return this;
        }

        public IMenu Build()
        {
            foreach (var type in _registredMenu)
            {
                Collect(_menu, type);
            }

            return _menu;
        }

        private void Collect(Menu menu, Type type)
        {
            var menuItems = type.GetMethods()
                .Where(m => m.GetCustomAttribute<MenuItemAttribute>() != null);

            var invalidMenuItem = menuItems
                .FirstOrDefault(m => m.GetParameters().Length > 0);

            if (invalidMenuItem != null)
            {
                throw new InvalidOperationException($"MenuItem handler {invalidMenuItem.DeclaringType.FullName}.{invalidMenuItem.Name} must not has parameters.");
            }

            foreach (var menuItem in menuItems)
            {
                var attr = menuItem.GetCustomAttribute<MenuItemAttribute>();
                menu.AddItem(new MenuItem(attr.Num, attr.Order, attr.Title, async () => {
                    var task = menuItem.Invoke(Activator.CreateInstance(type), new object[0]) as Task;
                    if (task != null)
                        await task;
                }));
            }

            foreach(var menuItem in _toAll.Except(menu.Items))
            {
                menu.AddItem(menuItem);
            }

            foreach (var sb in type.GetProperties()
                .Where(p => p.GetCustomAttribute<MenuItemAttribute>() != null)
                .Select(p => new { Type = p.PropertyType, Attribute = p.GetCustomAttribute<MenuItemAttribute>() }))
            {
                var innerMenu = new Menu(sb.Attribute.Num, sb.Attribute.Order, sb.Attribute.Title);
                menu.AddItem(innerMenu);
                Collect(innerMenu, sb.Type);
            }
        }
    }
}
