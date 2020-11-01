using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LFSR_SadConsole
{
    public class LFSR
    {
        private const uint VALUE_RANGE = 131072;
        private const uint LOW_BYTE = 255;
        private const uint HIGH_BYTE = 130816;
        private const uint SOUP_BYTE = 73728;
        private const uint INIT_BYTE = 1;

        private uint _randomValue = INIT_BYTE;
        private uint _cycles = default;
        private uint _x = default;
        private uint _y = default;
        private ValueTuple<(uint, uint)> _value;

        private readonly uint _width;
        private readonly uint _height;
        private readonly uint _dimension;

        public bool Finished => _cycles >= _dimension;
        public ValueTuple<(uint, uint)> Value => _value;
        public LFSR(int width, int height) 
        {
            _width = (uint)width;
            _height = (uint)height;
            _dimension = _width * _height;
        }
        public void Reset() 
        {
            _randomValue = INIT_BYTE;
            _cycles = default;
            _x = default;
            _y = default;
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
        private void Shift()
        {
            // Y = low 8 bits, 0 - 255 (i.e, 0000 0000 1111 1111)
            _y = (_randomValue & LOW_BYTE);

            // X = high 9 bits, 0 - 511 (i.e, 0000 0001 1111 1111) ~ after shift (>> 8)
            _x = ((_randomValue & HIGH_BYTE) >> 8);

            // get output bit
            uint lsb = _randomValue & 1;

            // shift register
            _randomValue >>= 1;

            // xor soup
            if (lsb != 0)
                _randomValue ^= SOUP_BYTE;

            _value = ValueTuple.Create<(uint, uint)>((_x, _y));

            return;
        }
        private void SyncNext()
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
        private async Task AsyncNext() 
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
