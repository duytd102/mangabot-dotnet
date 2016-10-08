using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Test
{
    class TruyenTranh8
    {
        public static string Run()
        {
            return Deobfuscating(
                "r e=t s();r c=t s();e[6]='b://1.g.j.k/-1H/1G-1F/1D/1E/h/1I-6.a?f=0';e[7]='b://1.g.j.k/-1J/1N/1M/1L/h/1K-7.a?f=0';e[18]='b://1.g.j.k/-1C-1B/1u/1t/1s/h/1q-18.a?f=0';e[24]='b://1.g.j.k/-1r/1v/1w/1A/h/1z-24.a?f=0';e[3]='b://1.g.j.k/-1y/1x/1O/1P/h/2c-3.a?f=0';e[20]='b://1.g.j.k/-2b/2a/29/2d/h/2e-20.a?f=0';e[8]='b://1.g.j.k/-2i/2h/2g/2f/h/28-8.a?f=0';e[4]='b://1.g.j.k/-27/1U/1T/1S/h/1Q-4.a?f=0';e[15]='b://1.g.j.k/-1R/1p-1W/26/1Z/h/1Y-15.a?f=0';e[19]='b://1.g.j.k/-1X/2j/I/H/h/G-19.a?f=0';e[2]='b://1.g.j.k/-F/K-E/N/M/h/L-2.a?f=0';e[21]='b://1.g.j.k/-X-O/D/w/x/h/v-21.a?f=0';e[13]='b://1.g.j.k/-y/C/B/A/h/z-13.a?f=0';e[10]='b://1.g.j.k/-J-1o/1h/1g/1f/h/1e-10.a?f=0';e[25]='b://1.g.j.k/-1i/1j/P/1n-1m/h/1l-1k.a?f=0';e[22]='b://1.g.j.k/-1d/1c/U/T/h/S-22.a?f=0';e[16]='b://1.g.j.k/-Q/R-V/W/1b/h/1a-16.a?f=0';e[14]='b://1.g.j.k/-Z/Y-1V/2m/39/h/38-14.a?f=0';e[9]='b://1.g.j.k/-37/3a/3b/3e/h/3d-9.a?f=0';e[11]='b://1.g.j.k/--2k/3c/36/35/h/2Z-11.a?f=0';e[23]='b://1.g.j.k/-2Y/2X/2W/30/h/3g-23.a?f=0';e[1]='b://1.g.j.k/-31/34/33/32/h/3f-1.a?f=0';e[0]='b://1.g.j.k/-3r/3n-3m/3k/3p/h/3h-%3t%3u%3l%3j.a?f=0';e[5]='b://1.g.j.k/-3i/3o/3s/3q/h/2U-5.a?f=0';e[12]='b://1.g.j.k/-2x/2w/2v/2y/h/2z-12.a?f=0';e[17]='b://1.g.j.k/-2B/2A/2u/2t/h/2n-17.a?f=0';c[10]='b://i.q.o/u/l/n-p-m/d/2V.a';c[4]='b://i.q.o/u/l/n-p-m/d/2l.a';c[23]='b://i.q.o/u/l/n-p-m/d/2o.a';c[7]='b://i.q.o/u/l/n-p-m/d/2p.a';c[1]='b://i.q.o/u/l/n-p-m/d/d.a';c[19]='b://i.q.o/u/l/n-p-m/d/2s.a';c[17]='b://i.q.o/u/l/n-p-m/d/2r.a';c[9]='b://i.q.o/u/l/n-p-m/d/2q.a';c[6]='b://i.q.o/u/l/n-p-m/d/2C.a';c[12]='b://i.q.o/u/l/n-p-m/d/2D.a';c[22]='b://i.q.o/u/l/n-p-m/d/2P.a';c[15]='b://i.q.o/u/l/n-p-m/d/2O.a';c[24]='b://i.q.o/u/l/n-p-m/d/2N.a';c[5]='b://i.q.o/u/l/n-p-m/d/2Q.a';c[13]='b://i.q.o/u/l/n-p-m/d/2R.a';c[18]='b://i.q.o/u/l/n-p-m/d/2T.a';c[14]='b://i.q.o/u/l/n-p-m/d/2S.a';c[25]='b://i.q.o/u/l/n-p-m/d/2M.a';c[20]='b://i.q.o/u/l/n-p-m/d/2L.a';c[16]='b://i.q.o/u/l/n-p-m/d/2G.a';c[8]='b://i.q.o/u/l/n-p-m/d/2F.a';c[21]='b://i.q.o/u/l/n-p-m/d/2E.a';c[3]='b://i.q.o/u/l/n-p-m/d/2H.a';c[2]='b://i.q.o/u/l/n-p-m/d/2I.a';c[0]='b://i.q.o/u/l/n-p-m/d/2K.a';c[11]='b://i.q.o/u/l/n-p-m/d/2J.a';", 
                62, 217, 
                "||||||||||jpg|https|lstImagesVIP|002|lstImages|imgmax|bp|s0|manga|blogspot|com|ngocph301|king|counterattack|net|demon|truyentranh8|var|Array|new||bt2938|AAAAAAAL4y4|CY4C8Jg0cDk|qAt8jj9Pl7s|bt2730|Y1hnJvpObWs|AAAAAAAL4x4|VqWzOvYDPfI|VqWzu7vxgTI|uN1tI|Kgd1ADDj5Hk|bt2907|mnOaK5EHusA|AAAAAAAL4yo|tu2Rd|VqWyd|bt2414|HPiwnh7pvCI|AAAAAAAL4wg|QVPqtq5hE|AAAAAAAL4zY|ctoIwvsL7vs|VqWzb01|bt2953|oGrgN7Q7qJ4|AAAAAAAL4zA|_1I|AAAAAAAL4yQ||VqWzTkf3|idugCvW5dXU|||||||||||bt2821|NvT3UQ0KPMs|VqWzykO83jI|SvdFz29CUEY|bt2633|inwgM1kV_lg|AAAAAAAL4xg|VqWzCpeiwkI|MR6EBVJrMsI|VqWz7oYfaeI|saddsadasdasdsa|bt3032|Ly0AI|_urfn|mNfHg|VqWzXi|bt2852|z1d5UKZ1DFA|wAeGIE_u0es|AAAAAAAL4yg|VqWzjORcAYI|VqWz6XZP9fI|AAAAAAAL4zQ|VqWyhoWQNhI|AKyRMqtBLBU|bt3024|WTzn0xjUI6A|RgA|yyMG8Zg|AAAAAAAL4xA|QEMpJ8B9Xw8|myFI|VqWysc|tSwiZelkXWk|bt2509|iyiwZCB2RE4|bt2527|UEIXBOu6u4Q|AAAAAAAL4xI|VqWyxT6j9ZI|AAAAAAAL4wo|7amv0CCQGwE|bt2442|d9hX2tJHKVM|g81sSbWKbuk|AAAAAAAL4ww|VqWyk4bC9HI|iI|6BdI|gkbPaCTZDx0|bt2806|fkBUA15bVE0|||||||AAAAAAAL4yI|4fGic35olog|bt2545|AAAAAAAL4yw|VqWzq5KqmPI|AMwghfUU0Bs|bt2429|FzPwbkVYRPw|bt2923|kzdD2LaX7No|AAAAAAAL4xQ|VqWy1uJLWHI|TVuZti2TNIE|VqWznKq94CI|PkUYpYDrOA|005|AAAAAAAL4yA|bt2839|024|008|010|018|020|3DT1jqXdmKw|AAAAAAAL4yY|AAAAAAAL4xw|VqWzK8b7ohI|YqrAdKqaT3A|XgsA7tyUrRs|bt2715|VqWzfwC6wvI|cAMsoSYUUJ8|007|013|022|009|017|004|003|012|001|021|026|025|016|023|006|014|015|019|bt2454|011|AAAAAAAL4zI|VqWz2ZjNKFI|eHcKfMfPjDI|bt2659|0oieHOJ2Kvg|a0KoU9q5EN8|Zt1iOXlYRyY|AAAAAAAL4wY|VqWyaB6CFCI|YqJj3KBDQwc|AAAAAAAL4xo|vLfKZ_SRcss|bt2748|wh0fSvZVg_Y|VqWy6zREDoI|AAAAAAAL4xY|VqWzHgNIY4I|bt2603|blNaLfc7G4g|bt2402|bt3009|bt2353|7g6QZjkbS5o|252529|AAAAAAAL4wQ|2525281|UNd3I|VqWyX|VqWyn24qPkI|jqtBFA0z4rc|21anDEZ8iho|d8BaWRTDkxE|AAAAAAAL4w4|25255BUNSET|25255D".Split('|'), 
                0, 
                new Dictionary<string, string>());
        }

        private static string Deobfuscating(string p, int a, int c, string[] k, int e, Dictionary<string, string> d)
        {
            while (--c >= 0)
            {
                if (c < k.Length && !string.IsNullOrEmpty(k[c]))
                {
                    d[fn2(c, a)] = k[c];
                }
                else
                {
                    d[fn2(c, a)] = fn2(c, a);
                }
            }
            c = 1;
            return new Regex(@"\b\w+\b").Replace(p, match => d[match.Value]);
        }

        private static string fn2(int c, int a)
        {
            return (c < a ? "" : fn2(c / a, a)) + ((c = c % a) > 35 ? Convert.ToChar(c + 29).ToString() : toRadix(c, 36));
        }

        /**
         * http://www.javascripter.net/faq/convert3.htm
         */
        private static string toRadix(decimal N, int radix)
        {
            string HexN = "";
            decimal Q = Math.Floor(Math.Abs(N));
            int R;
            while (true)
            {
                R = (int)Q % radix;
                HexN = "0123456789abcdefghijklmnopqrstuvwxyz"[R] + HexN;
                Q = (Q - R) / radix;
                if (Q == 0) break;
            }
            return ((N < 0) ? "-" + HexN : HexN);
        }
    }
}
