using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameServer;
using UnityGameServer.Networking;

namespace Example1 {
    public class ExampleServer : UnityServer<ExampleServer> {
        // access both base settings and your settings with this.
        public static Settings Settings { get { return (Settings)Instance.BaseSettings; } }

        public ExampleServer(string[] args) : base(args) {
        }

        // loads settings, called after constructor and before init.
        public override void LoadSettings(string file) {
            // don't call base.LoadSettings if you have your own settings class. instead
            // set BaseSettings to it.
            BaseSettings = new Settings(file);
            Logger.Log("Load settings.");
        }

        // initializes objects, called after Load settings and before update.
        public override void Init() {
            base.Init();
            Logger.Log("Init");
            CommandInput.LoadCommands(this); // add to load custom console commands.
        }

        // called every frame.
        public override void Update() {
            base.Update();
        }

        // base simply sets Run to false, and stops the loop.
        public override void Stop() {
            base.Stop();
            Logger.Log("Stop");
        }

        // disposes all objects, stops threads in task queue, etc. Also sets Run to false.
        public override void Dispose() {
            base.Dispose();
            Logger.Log("Dispose"); // not printed to console or logged.
        }

        // A test console command. Call test from the command line.
        [ConsoleCommand(1, "test", "[args]", "testing custom command.", "Tests custom command by printing what was entered.")]
        public object TestConsoleCMD(CommandContext context, params string[] args) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < args.Length; i++) {
                builder.Append(args[i] + " ");
            }
            string result = "Command entered: " + builder.ToString();
            Logger.Print(result);
            Logger.Log(result);
            Logger.LogWarning(result);
            Logger.LogError(result);
            return result;
        }

        // A test network command. Can be called from both tcp and udp.
        [Command(0x00)]
        public void Test_CMD(AsyncServer.SocketUser user, Data data) {
            Logger.Log("Data received: {0}", data.Input);
            user.Send(0x00, data.Input);
        }
    }
}
