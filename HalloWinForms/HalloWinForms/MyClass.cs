using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinForms
{
    class MyClass
    {

        private Größen mySize = Größen.Klein;

        public Größen GetGrößen()
        {
            return mySize;
        }
        internal void SetGrößen(Größen g)
        {
            mySize = g;
        }

        private int _myNumber; //backing field

        public int MyNumber //property (full)
        {
            get { return _myNumber; }
            internal set
            {
                if (value > 12)
                    _myNumber = value;
            }
        }


        //Auto Property
        public int EineZahl { get; set; }


        public MyClass(Größen mySize)
        {
            this.mySize = mySize;

            MyNumber = 14;
            Console.WriteLine(MyNumber);

            EineZahl = 99;
            Console.WriteLine(EineZahl);
        }

    }
}
