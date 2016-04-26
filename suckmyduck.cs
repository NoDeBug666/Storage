public static long res(long a,ref long bo,long val, long weight=1,int depth=0)
        {
            //Mod rest
            long times, distance;
            if (depth == 0)
                times = 1;
            else
                times = (Mod / a + (Mod % a == 1 ? 1 : 0)) * weight;
            if (depth == 0)
                distance = a;
            else
                distance = (times * a) % Mod;
            //Recursicve
            if (times < bo)
                val = res(a, ref bo, val, times,depth+1);
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
