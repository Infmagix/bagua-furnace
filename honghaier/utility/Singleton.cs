using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace honghaier.Utility {
    public class Singleton<T> where T : class, new() {
        private static volatile T instance;
        private static object locker = new Object();
        public Singleton() { }

        public static T Instance {
            get {
                if (instance == null) {
                    lock (locker) {
                        if (instance == null) instance = new T();
                    }
                }
                return instance;
            }
        }
    }
}
