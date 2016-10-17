using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    //(very) weak pseudorandom number generation
    //uses the system time as a seed
    public static class WeakRandom
    {
        private static int _x, _y, _z, _w;

        static WeakRandom()
        {
            unchecked
            {
                int now = (int)DateTime.Now.Ticks;
                _x = now;
                _y =  now * now;
                _z = now*314159265;
                _w = now*161803398;
            }
        } 
        public static int XorShift128()
        {
            int t = _x;
            t ^= t << 11;
            t ^= t >> 8;
            _x = _y;
            _y = _z;
            _z = _w;
            _w ^= _w >> 19;
            _w ^= t;
            return _w;
        }

        public static int XorShift128(int limit)
        {
            return XorShift128() % limit;
        }
    }
}
