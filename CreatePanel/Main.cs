using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace CreatePanel
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public string GetExeDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            return path;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Панель";
            application.CreateRibbonTab(tabName);

            string absPath = GetExeDirectory();

            string relPath1 = @"1\";
            string path1 = Path.Combine(absPath, relPath1);
            path1 = Path.GetFullPath(path1);

            string relPath2 = @"2\";
            string path2 = Path.Combine(absPath, relPath2);
            path2 = Path.GetFullPath(path2);

            var panel = application.CreateRibbonPanel(tabName, "Панель");

            var button_1 = new PushButtonData("Задание: Создание кнопок", "Создание\nпкнопок",
                Path.Combine(path1, "Creating_buttons.dll"),
                "Creating_buttons.Main");

            var button_2 = new PushButtonData("Задание: Изменение типов стен", "Изменение\nтипов стен",
                Path.Combine(path2, "ChangingWallTypes.dll"),
                "ChangingWallTypes.Main");

            panel.AddItem(button_1);
            panel.AddItem(button_2);

            return Result.Succeeded;
        }
    }
}