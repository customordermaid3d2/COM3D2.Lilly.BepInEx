using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 스레드 테스트
    /// </summary>
    public class ThreadPlugin : MonoBehaviour
    {
        static int cnt = 0;

        public ThreadPlugin()
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.LogMessage("GearMenuAddPlugin.OnSceneLoaded: " + scene.name + " , " + SceneManager.GetActiveScene().buildIndex + " , " + scene.isLoaded);
            // SceneManager.GetActiveScene().name;

            // Run 메서드를 입력받아
            // ThreadStart 델리게이트 타입 객체를 생성한 후
            // Thread 클래스 생성자에 전달
            //Thread t1 = new Thread(new ThreadStart(Run));
            Thread t1 = new Thread(new ParameterizedThreadStart(Run));
            t1.Name = "1";
            t1.Start(scene);
            // 안됨

            // 컴파일러가 Run() 메서드의 함수 프로토타입으로부터
            // ThreadStart Delegate객체를 추론하여 생성함
            Thread t2 = new Thread(Run);
            t2.Name = "2";
            t2.Start(scene);
            // 안됨

            // 익명메서드(Anonymous Method)를 사용하여
            // 쓰레드 생성
            Thread t3 = new Thread(delegate ()
            {
                Run(scene);
            });
            t3.Name = "3";
            t3.Start();

            // 람다식 (Lambda Expression)을 사용하여
            // 쓰레드 생성
            Thread t4 = new Thread(() => Run(scene));
            t4.Name = "4";
            t4.Start();

            //t4.Join(1000); 완료까지 대기. 확인 주기 밀리초

            // 간략한 표현
            //new Thread(() => Run(scene)).Start();

            //StartCoroutine(MyCoroutine()); // 실패

            //StartCoroutine(test(() => {
            //    MyLog.LogMessageS("StartCoroutine:"+ cnt++);
            //}));

            // yield return new WaitWhile(() => {  //true 인동안 대기
            //     return isFinish1.Contains<bool>(false);// 하나라도 진행중이면 대기
            // });

            // 이게 더 좋다고 함
            Task.Factory.StartNew(() => Run(scene));

            //======================================
            // https://docs.microsoft.com/ko-kr/dotnet/standard/parallel-programming/task-based-asynchronous-programming

            Thread.CurrentThread.Name = "Main";

            // Create a task and supply a user delegate by using a lambda expression.
            Task taskA = new Task(() => Console.WriteLine("Hello from taskA."));
            // Start the task.
            taskA.Start();

            // Output a message from the calling thread.
            Console.WriteLine("Hello from thread '{0}'.",
                              Thread.CurrentThread.Name);
            taskA.Wait();
            //======================================


            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((System.Object obj) =>
                {
                    var data = new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks };
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
                },
                                                     i);
            }
            Task.WaitAll(taskArray);

            //======================================


            //Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((System.Object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                },
                                                      new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
            }

            //======================================
        }
        class CustomData
        {
            public long CreationTime;
            public int Name;
            public int ThreadNum;
        }

        private System.Collections.IEnumerator test(Action action)
        {
            action();
            yield return null;
        }

        public System.Collections.IEnumerator MyCoroutine()
        {
            MyLog.LogMessage("ThreadPlugin.MyCoroutine st:" + Thread.CurrentThread.Name + " : " + cnt++);
            Thread.Sleep(1000);
            MyLog.LogMessage("ThreadPlugin.MyCoroutine ed:" + Thread.CurrentThread.Name + " : " + cnt++);
            yield return null;
        }

        void Run(object scene)
        {
            //if (scene is Scene)//안된다? scene.GetType()==typeof(Scene)  //상속 대비해서 is 사용
            {
                Run(scene);
            }
        }

        void Run(Scene scene)
        {
            MyLog.LogMessage("ThreadPlugin.Run st:" + Thread.CurrentThread.Name + " : " + cnt++);
            Thread.Sleep(1000);
            MyLog.LogMessage("ThreadPlugin.Run ed:" + Thread.CurrentThread.Name + " : " + cnt++);
        }
    }
}
