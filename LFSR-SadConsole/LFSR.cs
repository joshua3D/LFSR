using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LFSR_SadConsole
{
    public class LFSR
    {
        const uint VALUE_RANGE = 131072;
        const uint LOW_BYTE = 255;
        const uint HIGH_BYTE = 130816;
        const uint SOUP_BYTE = 73728;
        
        uint _x = default;
        uint _y = default;
        uint _cycles = 0;
        uint _randomValue = 1;

        readonly uint _width;
        readonly uint _height;
        readonly uint _dimension;

        public bool Finished => _cycles >= _dimension;
        public ValueTuple<(uint, uint)> Value => ValueTuple.Create<(uint, uint)>((_x, _y));
        public LFSR(int width, int height) 
        {
            _width = (uint)width;
            _height = (uint)height;
            _dimension = _width * _height;
        }
        public void Reset() 
        {
            _x = default;
            _y = default;
            _cycles = 1;
            _randomValue = 1;
        }

        public ValueTuple<(uint, uint)> Next()
        {
            SyncNext();

            return ValueTuple.Create<(uint, uint)>((_x, _y));
        }
        public async Task<ValueTuple<(uint, uint)>> AwaitNext() 
        {
            await AsyncNext();

            return ValueTuple.Create<(uint, uint)>((_x, _y));
        }
        ValueTuple<(uint, uint)> Shift()
        {
            // Y = low 8 bits
            _y = (_randomValue & LOW_BYTE);

            // X = high 9 bits
            _x = ((_randomValue & HIGH_BYTE) >> 8);

            // get output bit
            uint lsb = _randomValue & 1;

            // shift register
            _randomValue >>= 1;

            // xor soup
            if (lsb != 0)
                _randomValue ^= SOUP_BYTE;

            return ValueTuple.Create<(uint, uint)>((_x, _y));
        }
        void SyncNext()
        {
            uint i = default;

            while (true)
            {
                Shift();

                if (_x >= 0 && _x < _width)
                {
                    if (_y >= 0 && _y < _height)
                    {
                        _cycles += 1;
                        break;
                    }
                }

                if (i++ > VALUE_RANGE)
                {
                    break;
                }
            }
        }
        async Task AsyncNext() 
        {
            uint i = default;

            while (true)
            {
                Shift();

                if (_x >= 0 && _x <= _width)
                {
                    if (_y >= 0 && _y <= _height)
                    {
                        _cycles += 1;
                        break;
                    }
                }

                if (i++ > VALUE_RANGE)
                {
                    break;
                }
            }

            await Task.CompletedTask;
        }

        
    }
}
