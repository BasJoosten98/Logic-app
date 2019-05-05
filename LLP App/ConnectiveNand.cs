﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveNand : ConnectiveTwo
    {
        public ConnectiveNand()
        {

        }

        public override bool GetAnswer(TruthtableRow row)
        {
            bool leftAnswer = con1.GetAnswer(row);
            bool rightAnswer = con2.GetAnswer(row);
            if (!(leftAnswer && rightAnswer))
            {
                return true;
            }
            return false;
        }

        public override string GetInfix()
        {
            return "~(" + con1.GetInfix() + " & " + con2.GetInfix() + ")";
        }

        public override char GetLocalString()
        {
            return '%';
        }

        public override Connective GetNandProposition()
        {
            return this;
        }

        public override void setLeftConnective(Connective con)
        {
            if (con != null)
            {
                con1 = con;
            }
            else { throw new NullReferenceException(); }
        }

        public override void setRightConnective(Connective con)
        {
            if (con != null)
            {
                con2 = con;
            }
            else { throw new NullReferenceException(); }
        }
        public override string GetParseString()
        {
            return "%(" + con1.GetParseString() + "," + con2.GetParseString() + ")";
        }
    }
}
