using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Ui.Common
{
    public class MenuBuilder
    {
        private Dictionary<MenuItemAttribute, MethodInfo> _attrsInMethods = new Dictionary<MenuItemAttribute, MethodInfo>();
        private Dictionary<MenuItemAttribute, Type> _attrsInTypes = new Dictionary<MenuItemAttribute, Type>();

        public static MenuBuilder Default { get; } = new MenuBuilder();

        public MenuBuilder DetectMenuOn<T>()
        {
            var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.FlattenHierarchy;
            var methods = typeof(T).Assembly
                .GetTypes().SelectMany(t => t.GetMethods(flags)
                    .Where(m => m.GetCustomAttribute(typeof(MenuItemAttribute)) != null));

            _attrsInMethods = methods.ToDictionary(k => k.GetCustomAttribute<MenuItemAttribute>(), v => v);

            //var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.FlattenHierarchy;
            var types = typeof(T).Assembly
                .GetTypes().Where(m => m.GetCustomAttribute(typeof(MenuItemAttribute)) != null);

            //types.Any(t => !t.GetInterfaces().Any(typeof(IMenuHandler)));

            _attrsInTypes = types.ToDictionary(k => k.GetCustomAttribute<MenuItemAttribute>(), v => v);
            return this;
        }

        public void Build()
        {
            var menuItems = new List<MenuItem>();
            foreach (var attribute in _attrsInMethods.OrderBy(a => a.Key.Num))
            {
                menuItems.Add(new MenuItem(attribute.Key.Num, attribute.Key.Description, () => { attribute.Value.Invoke(null, new object[0]); }));
            }

            foreach (var attribute in _attrsInTypes.OrderBy(a => a.Key.Num))
            {
                menuItems.Add(new MenuItem(attribute.Key.Num, attribute.Key.Description, () => { ((IMenuHandler)Activator.CreateInstance(attribute.Value)).Handle(); }));
            }

            var menu = new Menu(menuItems.ToArray());
            menu.Process();
        }
    }
}
