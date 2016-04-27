/*
A * B mod C 破算法
*/
/*
實作方法：
利用連加取餘數的循環規則,配合上遞迴,
一次操作可以等於乘上多次,由於本方法有利用遞迴,
需要注意,可能導致Stack Overflow.

使用方法：
Result = res(A,ref B);
*/
long Mod = 1000000007;
static long res(long a, ref long bo, long val=0, long weight = 1, int depth = 0)
        {
            //Mod rest
            long times, distance;
            if (depth == 0)
                times = 1;
            else
                times = (Mod / a + (Mod % a == 0 ? 0 : 1)) * weight;
            if (depth == 0)
                distance = a;
            else
                distance = (times * a) % Mod;
            //Recursicve
            if (times < bo)
                val = res(a, ref bo, val, times, depth + 1);
            else if (times > bo && depth != 0)
                return val;
            //Remove Process
            while (bo >= times)
            {
                bo -= times;
                val += distance;
                val %= Mod;
            }
            return val;
        }
