using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КурсоваяООП
{
    class LZ77
    {
        static char[] buf = new char[3];
        public string Kod(string text)
        {
            int sm = 0;
            int kol = 0;
            string outp = "";
            List<(int, int, char)> list = new List<(int, int, char)>();
            string ou = "";
            string ou2 = "";
            buf[0] = text[0];
            buf[1] = text[1];
            buf[2] = text[2];
            int n = 3;
            ou += buf[0];
            do
            {
                if ((ou2.IndexOf(ou, 0, ou2.Length) == -1) | (ou2.Length == text.Length - 1))
                {
                    for (int i = 0; i < buf.Length - 1; i++) buf[i] = buf[i + 1];
                    if (n < text.Length) buf[buf.Length - 1] = text[n];
                    n++;
                    list.Add((sm, kol, ou[0]));
                    ou2 += ou;
                    ou = "";
                    ou += buf[0];
                }
                else
                {
                    if (ou2.Length != text.Length - 1)
                    {
                        do
                        {
                            for (int i = 0; i < buf.Length - 1; i++) buf[i] = buf[i + 1];
                            if (n < text.Length) buf[buf.Length - 1] = text[n];
                            n++;
                            ou += buf[0];
                        } while (ou2.IndexOf(ou, 0, ou2.Length) != -1);
                        ou = ou.Substring(0, ou.Length - 1);
                        if ((ou2 += ou) == text)
                        {
                            ou = ou.Substring(0, ou.Length - 1);
                            ou2 = ou2.Substring(0, ou2.Length - ou.Length - 1);
                        }
                        else ou2 = ou2.Substring(0, ou2.Length - ou.Length);
                        sm = ou2.IndexOf(ou, 0, ou2.Length);
                        kol = ou.Length;
                        list.Add((sm, kol, buf[0]));
                        ou2 += ou;
                        ou2 += buf[0];
                        for (int i = 0; i < buf.Length - 1; i++) buf[i] = buf[i + 1];
                        if (n < text.Length) buf[buf.Length - 1] = text[n];
                        n++;
                        ou = "";
                        ou += buf[0];
                        sm = 0;
                        kol = 0;
                    }
                    else
                    {
                        ou2 += ou;
                        list.Add((sm, kol, buf[0]));
                    }
                }
            } while (ou2 != text);
            foreach ((int, int, char) x in list)
            {
                outp += $"({x.Item1},{x.Item2},{x.Item3})";
            }
            return outp;
        }

        public string Dekod(string text)
        {
            string outp = "";
            List<(int, int, char)> list = new List<(int, int, char)>();
            int sm = 0;
            int kol = 0;
            char s;
            string str = "";
            int i = 0;
            do
            {
                int j = 0;
                if (text[i] == '(')
                {
                    i++;
                    j = i;
                    do
                    {
                        str += text[j];
                        j++;
                        i++;
                    } while (text[j] != ',');
                    sm = Convert.ToInt32(str);
                    j++;
                    i++;
                    str = "";
                    do
                    {
                        str += text[j];
                        j++;
                        i++;
                    } while (text[j] != ',');
                    kol = Convert.ToInt32(str);
                    j++;
                    i++;
                    str = "";
                    s = text[j];
                    i++;
                    i++;
                    str = "";
                    list.Add((sm, kol, s));
                }
            } while (i != text.Length);
            foreach ((int, int, char) x in list)
            {
                if (x.Item2 == 0) outp += x.Item3;
                else
                {
                    outp += outp.Substring(x.Item1, x.Item2);
                    outp += x.Item3;
                }
            }
            return outp;
        }
    }
}
