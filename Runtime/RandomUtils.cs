using System;
using System.Collections.Generic;

namespace NeedrunGameUtils
{
    public class RandomUtils
    {
        private static Random random = new Random();

        public static string RandomString(int size)
        {
            if (size > 32)
            {
                throw new Exception("RandomString method cannot create more than 32 characters.");
            }
            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString();
            return HashUtils.Md5(uuid).Substring(0, size);
        }

        public static string UUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static int Next()
        {
            return random.Next();
        }

        public static int PickIndexByWeight(List<int> weights)
        {
            // weights에 들어간 값이 0, 3, 12, 45라면, 총합인 60을 구한다.
            int totalWeight = 0;
            foreach (int weight in weights)
            {
                totalWeight += weight;
            }
            // randomValue는 [0, totalWeight) 값이다. (즉 59는 안나옴)
            int randomValue = UnityEngine.Random.Range(0, totalWeight);

            // case 1
            // 만약 사용자가 뽑은 값이 35라면, 이 값은 
            // 35              = 35는 0 보다 작지 않으므로 0번째 인덱스는 아니다
            // 35 - 0          = 35은 3 보다 작지 않으므로 1번째 인덱스는 아니다
            // 35 - 0 - 3      = 32은 12 보다 작지 않으므로 2번째 인덱스는 아니다
            // 35 - 0 - 3 - 12 = 20은 45 보다 작으므로 3번째 인덱스다
            //
            // case 2
            // 만약 사용자가 뽑은 값이 11이라면, 이 값은 
            // 11              = 11는 0 보다 작지 않으므로 0번째 인덱스는 아니다
            // 11 - 0          = 11은 3 보다 작지 않으므로 1번째 인덱스는 아니다
            // 11 - 0 - 3      =  8은 12 보다 작으므로 2번째 인덱스다
            //
            // case 3
            // 만약 사용자가 뽑은 값이 0이라면, 이 값은 
            // 0               = 0은 0 보다 작지 않으므로 0번째 인덱스는 아니다
            // 0 - 0           = 0은 3 보다 작으므로 1번째 인덱스다
            int originRandomValue = randomValue;
            for (int i = 0; i < weights.Count; i++)
            {
                //UnityEngine.Debug.Log(string.Format("process - origin: {0} -> {1}, {2}", originRandomValue, randomValue, randomValue < weights[i]));
                if (randomValue < weights[i])
                {
                    //UnityEngine.Debug.Log(string.Format("pick {0}", i));
                    return i;
                }
                randomValue -= weights[i];
            }
            // 이런 경우가 나와선 안됨 (단, 이런 경우 발생할 수 있음. ex. weight 안의 모든 값이 0인 경우)
            throw new Exception("Invalid weights");
        }

        // [0, max)
        public static int Next(int max)
        {
            return random.Next(max);
        }

        // [min, max)
        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }

        // [0, max)
        public static float Next(float max)
        {
            return (float)(random.NextDouble() * max);
        }

        public static T Choose<T>(List<Tuple<int, T>> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                return default(T);
            int max = 0;
            candidates.ForEach(v => max += v.Item1);
            int randomIndex = Next(max);
            for (int i = 0; i < candidates.Count; i++)
            {
                Tuple<int, T> candidate = candidates[i];
                randomIndex -= candidate.Item1;
                if (randomIndex <= 0)
                {
                    return candidate.Item2;
                }
            }
            return default(T);
        }

        public static HashSet<int> Pick(int max, int pickCount)
        {
            return Pick(0, max, pickCount);
        }

        public static HashSet<int> Pick(int max, int pickCount, HashSet<int> set)
        {
            return Pick(0, max, pickCount, set);
        }

        public static HashSet<int> Pick(int min, int max, int pickCount)
        {
            HashSet<int> set = new HashSet<int>();
            return Pick(min, max, pickCount, set);
        }

        public static HashSet<int> Pick(int min, int max, int pickCount, HashSet<int> set)
        {
            if (pickCount - set.Count > max - min)
                throw new Exception("Pick count " + pickCount + " is upper than min: " + min + ", max: " + max);
            while (set.Count < pickCount)
            {
                set.Add(RandomUtils.Next(min, max));
            }
            return set;
        }
    }
}
