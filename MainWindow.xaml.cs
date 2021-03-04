using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace metodo_de_muller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private IList<Iteracion> iteraciones;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtX0.Text) && !string.IsNullOrEmpty(TxtX1.Text))
            {
                if (double.TryParse(TxtX0.Text, out double x0) && double.TryParse(TxtX1.Text, out double x1))
                {
                    Iteracion iteracion, iteracionAnterior;
                    int i;

                    iteracionAnterior = new Iteracion();
                    iteraciones = new List<Iteracion>();
                    i = 0;

                    do
                    {
                        iteracion = new Iteracion();
                        iteracion.I = i;

                        if (iteracion.I == 0)
                        {
                            iteracion.X0 = x0;
                            iteracion.X1 = x1;
                            iteracion.X2 = (iteracion.X0 + iteracion.X1) / 2;
                        }
                        else
                        {
                            iteracion.X0 = iteracionAnterior.X1;
                            iteracion.X1 = iteracionAnterior.X2;
                            iteracion.X2 = iteracionAnterior.X3;
                        }
                        
                        iteracion.FX0 = Math.Pow(iteracion.X0, 4) - (3 * Math.Pow(iteracion.X0, 3)) + Math.Pow(iteracion.X0, 2) + iteracion.X0 + 1;
                        iteracion.FX1 = Math.Pow(iteracion.X1, 4) - (3 * Math.Pow(iteracion.X1, 3)) + Math.Pow(iteracion.X1, 2) + iteracion.X1 + 1;
                        iteracion.FX2 = Math.Pow(iteracion.X2, 4) - (3 * Math.Pow(iteracion.X2, 3)) + Math.Pow(iteracion.X2, 2) + iteracion.X2 + 1;
                        iteracion.H0 = iteracion.X1 - iteracion.X0;
                        iteracion.H1 = iteracion.X2 - iteracion.X1;
                        iteracion.Delta0 = (iteracion.FX1 - iteracion.FX0) / iteracion.H0;
                        iteracion.Delta1 = (iteracion.FX2 - iteracion.FX1) / iteracion.H1;
                        iteracion.A = (iteracion.Delta1 - iteracion.Delta0) / (iteracion.H1 - iteracion.H0);
                        iteracion.B = (iteracion.A * iteracion.H1) + iteracion.Delta1;
                        iteracion.C = iteracion.FX2;

                        if (iteracion.B > 0)
                        {
                            iteracion.X3 = iteracion.X2 + (-2 * iteracion.C / (iteracion.B + Math.Sqrt(Math.Pow(iteracion.B, 2) - (4 * iteracion.A * iteracion.C))));
                        }
                        else
                        {
                            iteracion.X3 = iteracion.X2 + (-2 * iteracion.C / (iteracion.B - Math.Sqrt(Math.Pow(iteracion.B, 2) - (4 * iteracion.A * iteracion.C))));
                        }

                        iteracion.Error = Math.Abs((iteracion.X3 - iteracionAnterior.X3) / iteracion.X3) * 100;

                        iteraciones.Add(iteracion);
                        iteracionAnterior = iteracion;
                        i++;

                    } while (iteracion.Error != 0 && iteracion.Error != 0.001);

                    DgIteraciones.ItemsSource = iteraciones;
                }
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtX0.Clear();
            TxtX1.Clear();
            DgIteraciones.ItemsSource = null;
            TxtX0.Focus();
        }
    }

}