using System.Collections.Generic;

namespace MVVM.Extensions
{
    public static class ViewModelBaseExtensions
    {
        public static void Test(this ViewModelBase origin)
        {
            return;
        }
        public static T Ancestors<T>(this ViewModelBase origin) where T : ViewModelBase
        {
            if (origin == null)
            {
                return null;
            }
            var parentViewModel = origin.ParentViewModel;
            while (parentViewModel != null)
            {
                var caster = parentViewModel as T;
                if (caster != null)
                {
                    return caster;
                }

                parentViewModel = parentViewModel.ParentViewModel;
            }

            return null;
        }
    }
}