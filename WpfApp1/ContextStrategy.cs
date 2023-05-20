﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ContextStrategy
    {
        private IStrategy _strategy;

        public void setStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }
        
        public void executeStrategy(Field area)
        {
           _strategy.executeStrategy(area);
        }

    }
}
