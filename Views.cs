namespace FileZillaConfig.Views {
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal static class Settings {
        private const int MAX_COLUMNS = 3;
        private static string[] EXCEPTIONS = new string[] { "CustomPASVIPServer" , "InitialWelcomeMessage" };

        internal static StackPanel Panel(int maxColumns=MAX_COLUMNS) {
            var properties = typeof(Poco.Settings).GetProperties();
            var stackpanel = new StackPanel();

            var regular = properties.Where(p => !EXCEPTIONS.Contains(p.Name));
            stackpanel.Children.Add(_Grid(regular , maxColumns , 50));

            var exceptions = properties.Where(p => EXCEPTIONS.Contains(p.Name));
            stackpanel.Children.Add(_Grid(exceptions , 1));

            return stackpanel;
        }

        private static Grid _Grid(IEnumerable<PropertyInfo> properties , int maxColumns , int secondColWidth=0) {
            var grid = new Grid();

            // calculate the number of rows and columns for the grid
            var count    = properties.Count();
            var num_rows = count/maxColumns + 1;
            if(count % maxColumns == 0) num_rows--;

            for(var i=0 ; i<num_rows ; i++){
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for(var j=0 ; j<maxColumns ; j++) {
                var col1 = new ColumnDefinition();
                col1.MinWidth = 150;
                col1.MaxWidth = 150;
                grid.ColumnDefinitions.Add(col1);

                var col2 = new ColumnDefinition();
                if(secondColWidth != 0) {
                    col2.MinWidth = secondColWidth;
                    col2.MaxWidth = secondColWidth;
                }
                grid.ColumnDefinitions.Add(col2);
            }

            // fill the grid
            var row = 0;
            var col = 0;
            foreach(var property in properties) {
                var label = _Label(property.Name , row , col*2);
                grid.Children.Add(label);

                var textBox = _TextBox(property , row , col * 2 + 1);
                grid.Children.Add(textBox);

                col++;
                if(col >= maxColumns) {
                    row++;
                    col = 0;
                }
            }

            return grid;
        }
        private static Label _Label(string content , int row , int col) {
            var label     = new Label();
            label.Content = content;
            Grid.SetRow(label , row);
            Grid.SetColumn(label , col);
            return label;
        }
        private static TextBox _TextBox(PropertyInfo property , int row , int col) {
            var textbox           = new TextBox();
            textbox.AcceptsReturn = true;
            textbox.Focusable     = false;
            Grid.SetRow(textbox , row);
            Grid.SetColumn(textbox , col);
            var path    = _PropertyPath(property);
            var binding = new Binding(path);
            var dp      = System.Windows.Controls.TextBox.TextProperty;
            BindingOperations.SetBinding(textbox , dp , binding);
            return textbox;
        }
        private static string _PropertyPath(PropertyInfo property) {
            var parent = property.DeclaringType;
            var parent_name = parent==null ? "" : _TypePath(parent) + ".";
            var name = parent_name + property.Name;
            return name;
        }
        private static string _TypePath(System.Type type) {
            var parent = type.DeclaringType;
            var parent_name = parent==null ? "" : _TypePath(parent) + ".";
            var name = parent_name + type.Name;
            return name;
        }
    }
}
