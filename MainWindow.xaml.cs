using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RavenDbEmbeddedTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                using (var session = DocumentSessionFactory.CreateSession())
                {
                    DataGrid.ItemsSource = session.Query<ObjectModel>()
                        .Customize(c => c.WaitForNonStaleResultsAsOfLastWrite())
                        .ToList();
                }
            }
            finally
            {
                Cursor = null;
            }
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                // Generate some data
                using (var session = DocumentSessionFactory.CreateSession())
                {
                    // Delete existing objects
                    session.Query<ObjectModel>().ToList().ForEach(o => session.Delete(o.Id));

                    session.SaveChanges();

                    // Generate new documents
                    for (int i = 0; i < 20; i++)
                    {
                        var objectModel = new ObjectModel
                        {
                            ObjectName = Guid.NewGuid().ToString(),
                            Name = Guid.NewGuid().ToString(),
                            Added = DateTime.Now
                        };
                        session.Store(objectModel, objectModel.Id);
                    }

                    session.SaveChanges();
                }
            }
            finally
            {
                Cursor = null;
            }
        }
    }
}
