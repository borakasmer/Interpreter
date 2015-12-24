using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interpreter.Models
{
    public static class Operation
    {
        static List<RoleExpression> CreateExpressionTree(string Formula)
        {
            Dictionary<string, int> RomNumber = new Dictionary<string, int>();
            RomNumber.Add("M", 1000);
            RomNumber.Add("D", 500);
            RomNumber.Add("C", 100);
            RomNumber.Add("L", 50);
            RomNumber.Add("X", 10);
            RomNumber.Add("V", 5);
            RomNumber.Add("I", 1);
            List<RoleExpression> tree = new List<RoleExpression>();

            char[] chrDizi = Formula.ToCharArray();
            Array.Reverse(chrDizi);

            for (int i = 0; i < chrDizi.Length; i++)
            {
                switch (chrDizi[i])
                {
                    case 'M':
                        {
                            tree.Add(new ThousendExpression());
                            break;
                        }
                    case 'D':
                        {
                            if (i > 0 && RomNumber[chrDizi[i].ToString()] >= RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiveHunderdExpression());
                            else if (i > 0 && RomNumber[chrDizi[i].ToString()] < RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiveHunderdMinuesExpression());
                            else if (i == 0)
                                tree.Add(new FiveHunderdExpression());
                            //else if (RomNumber[chrDizi[i].ToString()] == RomNumber[chrDizi[i - 1].ToString()])
                            //{
                            //    return new List<RoleExpression>();
                            //}
                            break;
                        }
                    case 'C':
                        {
                            if (i > 0 && RomNumber[chrDizi[i].ToString()] >= RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new HunderdExpression());
                            else if (i > 0 && RomNumber[chrDizi[i].ToString()] < RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new HunderdMinuesExpression());
                            else if (i == 0)
                                tree.Add(new HunderdExpression());
                            break;
                        }
                    case 'L':
                        {
                            if (i > 0 && RomNumber[chrDizi[i].ToString()] >= RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiftyExpression());
                            else if (i > 0 && RomNumber[chrDizi[i].ToString()] < RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiftyMinuesExpression());
                            else if (i == 0)
                                tree.Add(new FiftyExpression());
                            //else if (RomNumber[chrDizi[i].ToString()] == RomNumber[chrDizi[i - 1].ToString()])
                            //{
                            //    return new List<RoleExpression>();
                            //}
                            break;
                        }
                    case 'X':
                        {
                            if (i > 0 && RomNumber[chrDizi[i].ToString()] >= RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new TenExpression());
                            else if (i > 0 && RomNumber[chrDizi[i].ToString()] < RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new TenMinuesExpression());
                            else if (i == 0)
                                tree.Add(new TenExpression());
                            break;
                        }
                    case 'V':
                        {
                            if (i > 0 && RomNumber[chrDizi[i].ToString()] >= RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiveExpression());
                            else if (i > 0 && RomNumber[chrDizi[i].ToString()] < RomNumber[chrDizi[i - 1].ToString()])
                                tree.Add(new FiveMinuesExpression());
                            else if (i == 0)
                                tree.Add(new FiveExpression());
                            //else if (RomNumber[chrDizi[i].ToString()] == RomNumber[chrDizi[i - 1].ToString()])
                            //{
                            //    return new List<RoleExpression>();
                            //}
                            break;
                        }
                    case 'I':
                        {
                            if (i > 0)
                            {
                                bool boolMines = false;
                                for (int s = i - 1; s >= 0; s--)
                                {
                                    if (chrDizi[s].ToString() != "I")
                                        boolMines = true;
                                    break;
                                }
                                if (boolMines)
                                {
                                    tree.Add(new MinuesOneExpression());
                                }
                                else
                                    tree.Add(new PlusOneExpression());
                            }
                            else if (i == 0)
                            {
                                tree.Add(new PlusOneExpression());
                            }
                            break;
                        }
                }
            }
            return tree;
        }

        public static int RunExpression(Context context)
        {
            //int sameCount = 1;
            //RoleExpression oldRoleType=null;
            foreach (RoleExpression rex in CreateExpressionTree(context.Formula))
            {
                //if (oldRoleType != null)
                //{
                //    if (oldRoleType.GetType() == rex.GetType())
                //    {
                //        sameCount++;
                //        oldRoleType = rex;
                //    }
                //    else
                //    {
                //         sameCount=1;
                //        oldRoleType = rex;
                //    }
                //}
                //else
                //{
                //    oldRoleType = rex;
                //}
                //if (sameCount > 4) { return 0; }
                rex.Interpret(context);               
            }
            return  context.NumberValue;
        }
    }
    public class Context
    {
        public string Formula { get; set; }
        public int NumberValue { get; set; }
    }
    abstract class RoleExpression
    {
        public abstract void Interpret(Context context);
    }

    class ThousendExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('M'))
                context.NumberValue += 1000;
        }
    }
    class FiveHunderdExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('D'))
                context.NumberValue += 500;
        }
    }
    class FiveHunderdMinuesExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('D'))
                context.NumberValue -= 500;
        }
    }
    class HunderdExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('C'))
                context.NumberValue += 100;
        }
    }
    class HunderdMinuesExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('C'))
                context.NumberValue -= 100;
        }
    }
    class FiftyExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('L'))
                context.NumberValue += 50;
        }
    }
    class FiftyMinuesExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('L'))
                context.NumberValue -= 50;
        }
    }
    class TenExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('X'))
                context.NumberValue += 10;
        }
    }
    class TenMinuesExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('X'))
                context.NumberValue -= 10;
        }
    }
    class FiveExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('V'))
                context.NumberValue += 5;
        }
    }
    class FiveMinuesExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('V'))
                context.NumberValue -= 5;
        }
    }
    class PlusOneExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('I'))
                context.NumberValue += 1;
        }
    }
    class MinuesOneExpression : RoleExpression
    {
        public override void Interpret(Context context)
        {
            if (context.Formula.Contains('I'))
                context.NumberValue -= 1;
        }
    }
}