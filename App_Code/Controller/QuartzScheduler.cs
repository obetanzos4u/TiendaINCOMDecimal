using Quartz;
using Quartz.Impl;

/// <summary>
/// Programación de eventos a través de Quartz
/// </summary>
public class QuartzScheduler
{
    public static void Start()
    {
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
        scheduler.Start();

        IJobDetail jobDetail = JobBuilder.Create<SimpleJob>().Build();

        ITrigger trigger = TriggerBuilder
            .Create()
            .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(300) //s.WithIntervalInHours(24)
                .OnEveryDay()
                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 43)))
            .Build();

        scheduler.ScheduleJob(jobDetail, trigger);
    }
}
//public class QuartzScheduler
//{
//    private IScheduler _scheduler;
//    public QuartzScheduler()
//    {
//        _scheduler = ConfigureQuartz();
//    }
//    public IScheduler ConfigureQuartz()
//    {
//        NameValueCollection props = new NameValueCollection
//        {
//            { "quartz.serializer.type", "binary" }
//        };
//        StdSchedulerFactory factory = new StdSchedulerFactory(props);
//        var scheduler = factory.GetScheduler().Result;
//        scheduler.Start().Wait();
//        return scheduler;
//    }
//    private void OnShutDown()
//    {
//        if (!_scheduler.IsShutdown)
//        {
//            _scheduler.Shutdown();
//        }
//    }
//}