using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ScheduleService<T>
    {
        private readonly List<T> _items;
        private List<T> _currentConfiguration;
        public ScheduleService(IEnumerable<T> items)
        {
            _items = items.ToList();
            if (_items.Count % 2 != 0) throw new ArgumentException("Invalid length");
            _currentConfiguration = new List<T>(_items);
        }

        public void Swap(int indexA, int indexB)
        {
            T tmp = _currentConfiguration[indexA];
            _currentConfiguration[indexA] = _currentConfiguration[indexB];
            _currentConfiguration[indexB] = tmp;
        }

        public void Print()
        {
            foreach (var item in _currentConfiguration)
            {
                Console.Write(item);
                Console.Write(" ");
            }
            Console.Write("\n");
        }

        public void TablePrint()
        {
            for (var i = 0; i < _currentConfiguration.Count / 2; i++)
            {
                Console.Write(_currentConfiguration[i]);
                Console.Write(" ");
            }

            Console.Write("\n");

            for (var i = _currentConfiguration.Count - 1; i >= _currentConfiguration.Count / 2; i--)
            {
                Console.Write(_currentConfiguration[i]);
                Console.Write(" ");
            }

            Console.Write("\n");
        }

        public void PairsPrint()
        {
            var half = _currentConfiguration.Count / 2;
            for (var i = 0; i < half; i++)
            {
                Console.Write($"{_currentConfiguration[i]} - {_currentConfiguration[2 * half - 1 - i]}");
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------");
        }

        public List<Tuple<T, T>> PairsConstruct()
        {
            var half = _currentConfiguration.Count / 2;
            var pairs = new List<Tuple<T, T>>();

            for (var i = 0; i < half; i++)
            {
                pairs.Add(new Tuple<T, T>(_currentConfiguration[i], _currentConfiguration[2 * half - 1 - i]));
            }

            return pairs;
        }


        public void RoundPrint(int n = 1)
        {
            for (var i = (_currentConfiguration.Count - 1) * (n - 1) + 1; i <= (_currentConfiguration.Count - 1) * n; i++)
            {
                Console.WriteLine($"Тур {i}");
                PairsPrint();
                Rotate();
            }
        }

        public List<Tuple<T, T>> RoundConstruct(int n = 1)
        {
            var pairs = new List<Tuple<T, T>>();
            for (var i = (_currentConfiguration.Count - 1) * (n - 1) + 1; i <= (_currentConfiguration.Count - 1) * n; i++)
            {
                pairs = pairs.Concat(PairsConstruct()).ToList();
                Rotate();
            }

            return pairs;
        }

        public void RoundsPrint(int n)
        {
            for (var i = 1; i <= n; i++)
            {
                RoundPrint(i);
                SwitchAwayHome();
            }
        }

        public List<Tuple<T, T>> RoundsConstruct(int n)
        {
            var pairs = new List<Tuple<T, T>>();
            for (var i = 1; i <= n; i++)
            {
                pairs = pairs.Concat(RoundConstruct(i)).ToList();
                SwitchAwayHome();
            }
            return pairs;
        }

        public void Rotate()
        {
            var rotated = new List<T>(_currentConfiguration);

            for (var i = 2; i < _currentConfiguration.Count; i++)
            {
                rotated[i] = _currentConfiguration[i - 1];
            }

            rotated[1] = _currentConfiguration[_currentConfiguration.Count - 1];

            _currentConfiguration = rotated;
        }

        public void Rotate(int n)
        {
            for (var i = 1; i <= n; i++)
            {
                Rotate();
            }
        }

        public void Restart()
        {
            _currentConfiguration = new List<T>(_items);
        }

        public void SetTour(int n)
        {
            Rotate(n - 1);
        }

        public void SwitchAwayHome()
        {
            for (var i = 0; i < _currentConfiguration.Count / 2; i++)
            {
                var temp = _currentConfiguration[i];
                _currentConfiguration[i] = _currentConfiguration[_currentConfiguration.Count - i - 1];
                _currentConfiguration[_currentConfiguration.Count - i - 1] = temp;
            }
        }
    }
}
