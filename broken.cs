#define mapx
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 過程:
 * 1.取得輸入
 * 2.從第一個格子開始枚舉
 * 3.枚舉出後分辨其所屬系統
 * 4.運算 a ^ b mod c (平方項小可以不用代特殊公式)
 * 5.統整答案,並mod 1,000,000,007 (10E9+7)
 * 6.標記此格的轉角度記數
 * 7.統整答案,並mod 1,000,000,007 (10E9+7)
 * 8.解決問題
 */

namespace khcodeTeam04Enum
{
    class Program
    {
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        //南無大慈大悲救苦救難廣大靈感觀世音菩薩
        static int[] c;
        static int[] bt;
        static int[] len;
        static int[,] map;
        static int h, w,H,W,Hbound,Wbound,Ways;
        const int Mod = 1000000007;
        static long TextSize = -1;

        
        static int sm(int a, int b)
        {
            long num = 1;
            while (b-- > 0)
            {
                num *= a;
                num %= Mod;
            }
            return checked((int)num);
        }
        static long Bound(long b)
        {
            if (bt[b] == 1)
                return 1;

            int length = len[b*2]-1;

            if (bt[b] == 2)
                return (int)checked((length * 2) % Mod + 1);
            else if (bt[b] == 3)
                return (int)checked((((length * length) % Mod) * 3) + (length * 3) % Mod + 1);

            throw new Exception("Unexceped situation");
        }

        static void Main(string[] args)
        {
            int t = Int32.Parse(Console.ReadLine());
            while (t-- > 0)
            {
                //input
                string[] snum = Console.ReadLine().Split(' ');
                H = Int32.Parse(snum[0]);
                W = Int32.Parse(snum[1]);
                Hbound = H / 2 + H % 2;
                Wbound = W / 2 + W % 2;
                map = new int[H, W];
                //enum target
                for (h = 2; h <= Hbound; h++)
                {
                    for (w = 2; w <= Wbound; w++)
                    {
                        //initialization
                        #region 初始化
                        c = new int[4] {0,0,0,0};           //角落是否有碰觸到
                        bt = new int[4] { 1, 1, 1, 1 };     //和邊界碰觸的數量
                        len = new int[8] { h - 1, 0, W - w, 0, H - h, 0, w - 1,0 }; //八條邊多長
                        len[1] = h - 1 >= W - w ? W - w : h - 1;
                        len[3] = H - h >= W - w ? W - w : H - h;
                        len[5] = H - h >= w - 1 ? w - 1 : H - h;
                        len[7] = h - 1 >= w - 1 ? w - 1 : h - 1;
                        #endregion
                        //indenify
                        #region 辨識處理
                        if (h - 1 == W - w)
                        {
                            c[0] = 1;
                            bt[0]++;
                            bt[1]++;
                        }
                        else
                            bt[h - 1 > W - w ? 1 : 0]++;
                        
                        if (W - w == H - h)
                        {
                            c[1] = 1;
                            bt[1]++;
                            bt[2]++;
                        }else
                            bt[H - h > W - w ? 1 : 2]++;
                        if (w - 1 == H - h)
                        {
                            c[2] = 1;
                            bt[2]++;
                            bt[3]++;
                        }else
                            bt[w - 1 > H - h ? 2 : 3]++;
                        if (w - 1 == h - 1)
                        {
                            c[3] = 1;
                            bt[3]++;
                            bt[0]++;
                        }
                        else
                            bt[w - 1 > h - 1 ? 0 : 3]++;

                        int tag = c[0] + c[1] + c[2] + c[3];
                        if (tag == 0)
                            tag = 1;
                        else if (tag == 4)
                            tag = 5;
                        else if (tag == 1)
                            tag = 2;
                        else if (tag == 2 && ((c[0] == 1 && c[2] == 1) || (c[1] == 1 && c[3] == 1)))
                            tag = 6;
                        else if (tag == 2)
                            tag = 3;
                        else
                            throw new Exception("Unexceped situation");
                        #endregion
                        //collect
                        #region obov
                        int n = 0,n2 =0;
                        long temp=0,temp2=0;
                        switch (tag)
                        {
                            case 1:
                                #region NNNN
                                temp = checked(((((Bound(0) * Bound(1)) % Mod) * Bound(2)) % Mod) * Bound(3)) % Mod;
                                Ways += (int)temp;
                                Ways %= Mod;
                                #endregion
                                break;
                            case 2:
                                #region NNNA
                                {
                                    long bo = 0;
                                    temp2 = -1;
                                    for (int i = 0; i < 4; i++)
                                        temp2 = (c[i] == 1 ? i : temp2);
                                    n = len[temp2*2+1] - 1;
                                    if (temp2 == 0)
                                    {
                                        temp2 = bt[0] + bt[1]-(c[0] == 1 ? 1 : 0);
                                        bo = checked(Bound(2) * Bound(3)) % Mod;
                                    }
                                    else if (temp2 == 1)
                                    {
                                        temp2 = bt[1] + bt[2]-(c[1] == 1 ? 1 : 0);
                                        bo = checked(Bound(0) * Bound(3)) % Mod;
                                    }
                                    else if (temp2 == 2)
                                    {
                                        temp2 = bt[2] + bt[3]-(c[2] == 1 ? 1 : 0);
                                        bo = checked(Bound(1) * Bound(0)) % Mod;
                                    }
                                    else
                                    {
                                        temp2 = bt[3] + bt[0] - (c[3]==1 ? 1 : 0);
                                        bo = checked(Bound(1) * Bound(2)) % Mod;
                                    }

                                    if (temp2 == 3)
                                    {
                                        temp = 1;
                                        temp += checked(3 * sm(n, 1)); temp %= Mod;
                                        temp += checked(1 * sm(n, 2)); temp %= Mod;
                                    }
                                    else if (temp2 == 4)
                                    {
                                        temp = 1;
                                        temp += checked(4 * sm(n, 1)); temp %= Mod;
                                        temp += checked(5 * sm(n, 2)); temp %= Mod;
                                        temp += checked(1 * sm(n, 3)); temp %= Mod;
                                    }
                                    else if (temp2 == 5)
                                    {
                                        temp = 1;
                                        temp += checked(5 * sm(n, 1)); temp %= Mod;
                                        temp += checked(10 * sm(n, 2)); temp %= Mod;
                                        temp += checked(8 * sm(n, 3)); temp %= Mod;
                                        temp += checked(1 * sm(n, 4)); temp %= Mod;
                                    }
                                    else
                                        throw new Exception("");
                                    //Res 
                                    long SR, Sdis;
                                    SR = (Mod / temp + (Mod % temp == 0 ? 0 : 1));
                                    Sdis = checked(SR * temp - Mod);
                                    temp2 = 0;
                                    while (bo > 0)
                                    {
                                        if (bo >= SR)
                                        {
                                            bo -= SR;
                                            temp2 += Sdis;
                                            temp2 %= Mod;
                                        }
                                        else
                                        {
                                            bo--;
                                            temp2 += temp;
                                            temp2 %= Mod;
                                        }
                                    }
                                    temp = temp2;
                                    Ways += (int)temp; Ways %= Mod;
                                    map[h - 1, w - 1] = (int)temp;
                                }
                                #endregion
                                break;
                            case 3:
                                #region NNAA
                                {
                                    int Max=w-1,Min=w-1, MaxC=0, MinC=0,bo=3;
                                    for(int i = 0;i < 8;i++){
                                        if (len[i] > Max)
                                            Max = len[i];
                                        if (Min > len[i])
                                        {
                                            Min = len[i];
                                            if (i % 2 == 0)
                                                bo = i / 2;
                                        }
                                    }
                                    for (int i = 0; i < 8; i++)
                                        if (len[i] == Max)
                                            MaxC++;
                                        else if (len[i] == Min)
                                            MinC++;
                                    if (MaxC + MinC != 8)
                                        throw new Exception("Unexceped situation");
                                    if(MaxC == 1)
                                    {
                                        n = Min - 1;
                                        temp = 1;
                                        temp += 7 * sm(n, 1); temp %= Mod;
                                        temp += checked(21 * sm(n, 2)); temp %= Mod;
                                        for (int i = 0, add = sm(n, 3); i < 32; i++)
                                        { temp += add; temp %= Mod; }
                                        //temp += checked(32 * sm(n, 3)); temp %= Mod;
                                        for (int i = 0, add = sm(n, 4); i < 23; i++)
                                        { temp += add; temp %= Mod; }
                                        //temp += checked(23 * sm(n, 4)); temp %= Mod;
                                        for (int i = 0, add = sm(n, 5); i < 5; i++)
                                        { temp += add; temp %= Mod; }
                                        Ways += (int)temp; Ways %= Mod;
                                        map[h - 1, w - 1] = (int)temp;
                                    }
                                    else
                                    {
                                        n = Max - 1;
                                        temp = 1;
                                        temp += checked(5 * sm(n, 1)); temp %= Mod;
                                        temp += checked(8 * sm(n, 2)); temp %= Mod;
                                        temp += checked(3 * sm(n, 3)); temp %= Mod;
                                        temp *= Bound(bo)%Mod;
                                        Ways += (int)temp; Ways %= Mod;
                                        map[h - 1, w - 1] = (int)temp;
                                    }
                                }
                                #endregion
                                break;
                            case 5:
                                #region AAAA
                                n = w - 2;
                                temp = 1;
                                temp += checked(8 * sm(n, 1)) % Mod; temp %= Mod;
                                for (long i = 0, add = sm(n, 2); i < 28; i++)
                                 temp = checked(temp + add); temp %= Mod;
                                //temp += checked(28 * sm(n, 2)) % Mod; temp %= Mod;
                                for (long i = 0, add = sm(n, 3); i < 52; i++)
                                 temp = checked(temp + add); temp %= Mod;
                                //temp += checked(52 * sm(n, 3)) % Mod; temp %= Mod;
                                for (long i = 0, add = sm(n, 4); i < 50; i++)
                                 temp = checked(temp + add); temp %= Mod;
                                //temp += (50 * sm(n, 4)) % Mod; temp %= Mod;
                                for (long i = 0, add = sm(n, 5); i < 20; i++)
                                 temp = checked(temp + add); temp %= Mod;
                                //temp += (20 * sm(n, 5)) % Mod; temp %= Mod;
                                for (long i = 0, add = sm(n, 6); i < 2; i++)
                                 temp = checked(temp + add); temp %= Mod;
                                
                                Ways += (int)temp;
                                Ways %= Mod;
#endregion
                                break;
                            case 6:
                                #region NANA
                                if (c[0] == 1 && c[2] == 1)
                                    if (w -1> h-1)
                                    {
                                        n = len[1] - 1;
                                        n2 = len[5] - 1;
                                    }
                                    else
                                    {
                                        n = len[5] - 1;
                                        n2 = len[1] - 1;
                                    }
                                else
                                    if (h-1 > W - w)
                                    {
                                        n = len[3] - 1;
                                        n2 = len[7] - 1;
                                    }
                                    else
                                    {
                                        n = len[7] - 1;
                                        n2 = len[3] - 1;
                                    }
                                temp = 1;
                                temp += checked(5 * sm(n, 1)) % Mod; temp %= Mod;
                                temp += checked(10 * sm(n, 2)) % Mod; temp %= Mod;
                                temp += checked(8 * sm(n, 3)) % Mod; temp %= Mod;
                                temp += checked(1 * sm(n, 4)) % Mod; temp %= Mod;
                                temp2 = 1;
                                temp2 += checked(3 * sm(n2, 1)) % Mod; temp2 %= Mod;
                                temp2 += checked(1 * sm(n2, 2)) % Mod; temp2 %= Mod;
                                temp = (checked(temp2 * temp)%Mod);
                                Ways += (int)temp;
                                Ways %= Mod;
                                #endregion
                                break;
                            default:
                                throw new Exception("Unexceped situation");
                        }
                        map[h - 1, w - 1] = (int)temp;
                        #endregion
                        //reverse copy
                        #region 翻轉
                        temp2 = 1;
                        if (h != H / 2 + 1 && H % 2 == 1)
                            temp2 *= 2;
                        if (w != W / 2 + 1 && W % 2 == 1)
                            temp2 *= 2;
                        for(int i = 0;i < temp2-1;i++)
                        {
                            Ways += (int)temp;
                            Ways %= Mod;
                        }
                        #endregion
                        //Post
                        if (Math.Log10(temp) > TextSize)
                            TextSize = (int)Math.Log10(temp) + 2;
                        if (temp < 0)
                            throw new OverflowException();
                    }
                }
                //debug output
                #region 訊息輸出
#if map
                using (StreamWriter sr = new StreamWriter(String.Format("Output{0}_{1}_{2} {3}_{4}.txt",DateTime.Today.Day,DateTime.Today.Hour,DateTime.Today.Minute,H,W),false,Encoding.UTF8,1<<20))
                {
                    for (int i = 0; i < H/2+1; i++)
                    {
                        for (int j = 0; j < W/2+1; j++)
                        {
                            sr.Write("{0," + TextSize + "}", map[i, j]);
                            sr.Flush();
                        }
                        sr.WriteLine();
                    }
                }
#endif
                #endregion

                Console.WriteLine(Ways%Mod);
                Ways = 0;
            }
        }
    }
}
