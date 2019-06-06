using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class TableauxSetElement
    {
        private Connective element;

        public Connective Element { get { return element; } }

        public TableauxSetElement(Connective con)
        {
            this.element = con;
        }

        public TableauxSetElement Copy()
        {
            return new TableauxSetElement(element.Copy());
        }

        //NO SPLITTING OF TABLEAUXSET NEEDED
        public List<Connective> ApplyAlfaTableauxRules()
        {
            List<Connective> results = new List<Connective>();

            if (element is ConnectiveAnd) //AND RULE
            {
                ConnectiveAnd cn = (ConnectiveAnd)element;

                results.Add(cn.Con1.Copy());
                results.Add(cn.Con2.Copy());
            }
            else if (element is ConnectiveNot) //FIRST LAYER IS NOT
            {
                ConnectiveNot cn = (ConnectiveNot)element;

                if (cn.Con1 is ConnectiveNot) //DOUBLE NEGATION RULE
                {
                    ConnectiveNot cn1 = (ConnectiveNot)cn.Con1;

                    results.Add(cn1.Con1.Copy());
                }
                else if(cn.Con1 is ConnectiveOr) //SECOND LAYER IS OR
                {
                    ConnectiveOr cn1 = (ConnectiveOr)cn.Con1;

                    ConnectiveNot not1 = new ConnectiveNot();
                    ConnectiveNot not2 = new ConnectiveNot();
                    not1.setLeftConnective(cn1.Con1.Copy());
                    not2.setLeftConnective(cn1.Con2.Copy());
                    results.Add(not1);
                    results.Add(not2);
                }
                else if (cn.Con1 is ConnectiveImplication) //SECOND LAYER IS IMPLICATION
                {
                    ConnectiveImplication cn1 = (ConnectiveImplication)cn.Con1;

                    results.Add(cn1.Con1.Copy());
                    ConnectiveNot not = new ConnectiveNot();
                    not.setLeftConnective(cn1.Con2.Copy());
                    results.Add(not);
                }
                else if(cn.Con1 is ConnectiveNand)
                {
                    ConnectiveNand cn1 = (ConnectiveNand)cn.Con1;

                    results.Add(cn1.Con1.Copy());
                    results.Add(cn1.Con2.Copy());
                }
            }
            else if(element is ConnectiveBiImplication)
            {
                ConnectiveBiImplication cbi = (ConnectiveBiImplication)element;

                ConnectiveImplication imp1 = new ConnectiveImplication();
                ConnectiveImplication imp2 = new ConnectiveImplication();
                imp1.setLeftConnective(cbi.Con1.Copy());
                imp1.setRightConnective(cbi.Con2.Copy());
                imp2.setLeftConnective(cbi.Con2.Copy());
                imp2.setRightConnective(cbi.Con1.Copy());

                results.Add(imp1);
                results.Add(imp2);
            }

            return results;
        }

        //SPLITTING OF TABLEAUXSET NEEDED
        public List<Connective> ApplyBetaTableauxRules()
        {
            List<Connective> results = new List<Connective>();

            if (element is ConnectiveNot) //FIRST LAYER IS NOT
            {
                ConnectiveNot cn = (ConnectiveNot)element;

                if (cn.Con1 is ConnectiveAnd) //SECOND LAYER IS AND
                {
                    ConnectiveAnd cn1 = (ConnectiveAnd)cn.Con1;

                    ConnectiveNot not1 = new ConnectiveNot();
                    ConnectiveNot not2 = new ConnectiveNot();
                    not1.setLeftConnective(cn1.Con1.Copy());
                    not2.setLeftConnective(cn1.Con2.Copy());
                    results.Add(not1);
                    results.Add(not2);
                }
                else if(cn.Con1 is ConnectiveBiImplication) //SECOND LAYER IS BI-IMPLICATION
                {
                    ConnectiveBiImplication cn1 = (ConnectiveBiImplication)cn.Con1;

                    ConnectiveNot not1 = new ConnectiveNot();
                    ConnectiveNot not2 = new ConnectiveNot();
                    ConnectiveImplication imp1 = new ConnectiveImplication();
                    ConnectiveImplication imp2 = new ConnectiveImplication();
                    not1.setLeftConnective(imp1);
                    not2.setLeftConnective(imp2);
                    imp1.setLeftConnective(cn1.Con1.Copy());
                    imp1.setRightConnective(cn1.Con2.Copy());
                    imp2.setLeftConnective(cn1.Con2.Copy());
                    imp2.setRightConnective(cn1.Con1.Copy());

                    results.Add(not1);
                    results.Add(not2);
                }
            }
            else if (element is ConnectiveOr) //OR RULE
            {
                ConnectiveOr co = (ConnectiveOr)element;

                results.Add(co.Con1.Copy());
                results.Add(co.Con2.Copy());
            }
            else if (element is ConnectiveImplication) //IMPLICATION RULE
            {
                ConnectiveImplication ci = (ConnectiveImplication)element;

                ConnectiveNot not = new ConnectiveNot();
                not.setLeftConnective(ci.Con1.Copy());
                results.Add(not);
                results.Add(ci.Con2.Copy());
            }
            else if(element is ConnectiveNand)
            {
                ConnectiveNand cn = (ConnectiveNand)element;

                ConnectiveNot not1 = new ConnectiveNot();
                ConnectiveNot not2 = new ConnectiveNot();
                not1.setLeftConnective(cn.Con1.Copy());
                not2.setLeftConnective(cn.Con2.Copy());
                results.Add(not1);
                results.Add(not2);
            }

            return results;
        }

    }
}
