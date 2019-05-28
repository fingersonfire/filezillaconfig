namespace FileZillaConfig {
    using System;
    using System.ServiceProcess;

    internal static class Service {
        private static readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(2);

        public static void Restart() {
            try {
                var service = new ServiceController("FileZilla Server");

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, TIMEOUT);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running , TIMEOUT);
            } catch(Exception ex) {
                var msg = ex.Message;
            }
        }
    }
}
