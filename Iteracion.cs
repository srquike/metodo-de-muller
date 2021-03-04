using System;
using System.Collections.Generic;
using System.Text;

namespace metodo_de_muller
{
    class Iteracion
    {
        private double error;

        public int I { get; set; }
        public double X0 { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double FX0 { get; set; }
        public double FX1 { get; set; }
        public double FX2 { get; set; }
        public double H0 { get; set; }
        public double H1 { get; set; }
        public double Delta0 { get; set; }
        public double Delta1 { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double X3 { get; set; }
        public double Error { get => Math.Round(error, 3); set => error = value; }
    }
}
