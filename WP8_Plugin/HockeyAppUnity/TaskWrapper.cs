using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if (UNITY_WP8 && !UNITY_EDITOR)
using System.Threading.Tasks;
#endif

namespace HockeyAppUnity
{

    public class TaskWrapper<T>
    {
#if (UNITY_WP8 && !UNITY_EDITOR)
        public TaskWrapper(Task<T> task)
        {
            _wrappedTask = task;
        }
        Task<T> _wrappedTask;
#endif

        public T Result
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null ? _wrappedTask.Result : default(T) ;
#else 
            return default(T);
#endif
            }
        }


        public bool IsCompleted
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null && _wrappedTask.IsCompleted;
#else 
            return true;
#endif
            }
        }

        public bool IsCanceled
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null && _wrappedTask.IsCanceled;
#else 
            return false;
#endif
            }
        }

        public bool IsFaulted
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null && _wrappedTask.IsFaulted;
#else 
            return false;
#endif
            }
        }


    }

    public class TaskWrapper
    {
        #if (UNITY_WP8 && !UNITY_EDITOR)
        public TaskWrapper(Task task)
        {
            _wrappedTask = task;
        }
        Task _wrappedTask; 
        #endif

        public bool IsCompleted { get {
            #if (UNITY_WP8 && !UNITY_EDITOR)
            return _wrappedTask != null && _wrappedTask.IsCompleted;
            #else 
            return true;
            #endif
        }}

        public bool IsCanceled
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null && _wrappedTask.IsCanceled;
#else 
            return false;
#endif
            }
        }

        public bool IsFaulted
        {
            get
            {
#if (UNITY_WP8 && !UNITY_EDITOR)
                return _wrappedTask != null && _wrappedTask.IsFaulted;
#else 
            return false;
#endif
            }
        }


    }
}
