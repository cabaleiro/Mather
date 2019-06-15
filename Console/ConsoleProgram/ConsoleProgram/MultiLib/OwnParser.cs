using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleProgram.MultiLib
{
    class OwnParser
    {
        //Hard formatting input for this parser (Implies needing a proxy class)
        // y=bx*2*0.58*e^(x+2)-5
            //If no = sign is detected and no variables, operate
            //Already detects y and x as variables
            // y= is an idiom, calls function parser
            //b isn't a variable or number, goes to PARAMETER list
                //PARAMETER b
                //BIN *
                //VARIABLE x
                //BIN *
                //CONST 2
                //BIN *
                //CONST 0.58
                //BIN *
                //SPECIAL CONST e
                //BIN ^
                // WAIT
                //VARIABLE x
                //BIN +
                //CONST 2
                // END WAIT
                //BIN ^
                //RESULT
                // -
                // 5
        //MAINTHREAD:
            //TOKEN 1 (X+5) TO SUBTHREAD 1
                //TOKEN 1 = [token struct]
            //TOKEN 2 (2X/7) TO SUBTHREAD 2
            //TOKEN 3 (3b+2) TO SUBTHREAD 3
            

    }
}
