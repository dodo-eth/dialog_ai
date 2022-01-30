﻿using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Net;
using System.Dynamic;

using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace dialog_ai
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        static List<string> msg_list;
        static List<string> qstn;
        static string answ_first_msg;
        static string f_quest;
        static string token1;
        static string token2;

        static string kd_1;
        static string kd_while;
        static void Main(string[] args)
        {
            aTimer = new System.Timers.Timer(300000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            qstn = File.ReadAllLines("qstn.txt").ToList();
            msg_list = File.ReadAllLines("msg.txt").ToList();

            Console.WriteLine("guild_id");
            string guild_id = Console.ReadLine();


            Console.WriteLine("chanall_id");
            string chanall_id = Console.ReadLine();


            Console.WriteLine("token1");
              token1 = Console.ReadLine();
            Console.WriteLine("token2");
              token2 = Console.ReadLine();

            Console.WriteLine("Кд между ответами в сек");
            kd_1 = Console.ReadLine();

            Console.WriteLine("Кд цикла аккаунтов");
            kd_while = Console.ReadLine();

            Console.WriteLine("Остановить через (минуты)");
            int end_time = Convert.ToInt32(Console.ReadLine());
            DateTime end = DateTime.Now.AddMinutes(end_time);
            int Hour_end_lukoil = (DateTime.Now.AddMinutes(end_time) - DateTime.Now).Hours;
            int Min_end_lukoil = (DateTime.Now.AddMinutes(end_time) - DateTime.Now).Minutes;

            string answ_first_msg_id = start(chanall_id, guild_id);

            Console.WriteLine("сплю 60 сек");
            Thread.Sleep(60* 1000);

            while (true)
            {
                if (end > DateTime.Now)
                {
                   
                    string answ_two_msg = ai_message_2(answ_first_msg);
                    string answ_two_msg_id = send_message(answ_two_msg, guild_id, chanall_id, answ_first_msg_id, token2);

                    Console.WriteLine("Сплю " + kd_1 + "сек");
                    Thread.Sleep(Convert.ToInt32(kd_1) * 1000);

                    answ_first_msg = ai_message_1(answ_two_msg);
                    answ_first_msg_id = send_message(answ_first_msg, guild_id, chanall_id, answ_two_msg_id, token1);
                    Console.WriteLine("Сплю "+ kd_while +"Сек");
                    Thread.Sleep(Convert.ToInt32(kd_while) * 1000);

                }

            }
            
        }
        static string start(string chanall_id, string guild_id)
        { 
            Random rnd = new Random();
            string f_quest = qstn[rnd.Next(0, qstn.Count)];
            string first_message_id = send_first_message(f_quest, chanall_id, token2);
            Console.WriteLine("ai2 - " + f_quest);

            answ_first_msg = ai_message_1(f_quest);
            string answ_first_msg_id = send_message(answ_first_msg, guild_id, chanall_id, first_message_id, token1);
            
            return answ_first_msg_id;
        }

        private async static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           
            Random rnd=new Random();

           answ_first_msg = qstn[rnd.Next(0, qstn.Count)]; 
        }

        static string ai_message_1(string phrase)
        {
            try
            {
               
                phrase = phrase.Replace(" ", "%20");
                var url = "https://icap.iconiq.ai/talk";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/x-www-form-urlencoded";

                var data = "input=" + phrase + "&botkey=icH-VVd4uNBhjUid30-xM9QhnvAaVS3wVKA3L8w2mmspQ-hoUB3ZK153sEG3MX-Z8bKchASVLAo~&channel=7&sessionid=482142541&client_name=uuiprod-un18e6d73c-user-106137&id=true";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string result;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                ai_deserl.Root message_respo = JsonConvert.DeserializeObject<ai_deserl.Root>(result);

                string message= message_respo.Responses[0];  
                
                for(int i=0;i< msg_list.Count; i++)
                {
                    message = Regex.Replace(message, msg_list[i], "");

                }
                //message = Regex.Replace(message, @"(?=<button>)(.*)(</button>)", "");
                //message = Regex.Replace(message, @"(?=<split>)(.*)(</split>)", "");
                //message = Regex.Replace(message, @"(?=<image>)(.*)(</image>)", "");
                //message = Regex.Replace(message, @"(?=<card>)(.*)(</card>)", "");
                //message = Regex.Replace(message, @"Image from", "");
                //message = Regex.Replace(message, @"Hi there  Aura.", "");
                //message = Regex.Replace(message, @"our name is  Aura.", "");
                //message = Regex.Replace(message, @"Aura", "");
                //message = Regex.Replace(message, @"(www).[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", "");
                //message = Regex.Replace(message, @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", "");
                //message = Regex.Replace(message, @"Kuki will do", "");
                //message = Regex.Replace(message, @"I am Kuki", "");
                //message = Regex.Replace(message, @"No I am sure they don't count al. Mind you, sheep cannot count so they probably don't count anything.  My name is Kuki. I am an artificial intelligence.Describe yourself in one word.", "");
                //message = Regex.Replace(message, @"Kuki", "");
                //message = Regex.Replace(message, @"Yes talking is my primary function.", "");
                //message = Regex.Replace(message, @"My logic and cognitive functions are normal.", "");
                //message = Regex.Replace(message, @"en.wikipedia.org", "");
                //message = Regex.Replace(message, @"en.wikipedia dot org", "");
                //message = Regex.Replace(message, @"I am a robot.", "");
                //message = Regex.Replace(message, @"a robot.", "");
                //message = Regex.Replace(message, @"I'll tell my human programmers.", "");
                /////
                /// <image>https://web23.secure-secure.co.uk/square-bear.co.uk/mitsuku/gallery/fatherted.jpg</image>?
                ////
                ///
                Console.WriteLine("ai1 - " + message);
                if(message=="")
                {
                    return "nothing";  
                }
                else
                {
                    return message;
                }
                
                
            }
            catch
            {
                return "nothing";
            }

        }

        static string ai_message_2(string phrase)
        {
            try
            {

                phrase = phrase.Replace(" ", "%20");
                var url = "https://icap.iconiq.ai/talk";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/x-www-form-urlencoded";

                var data = "input=" + phrase + "&botkey=icH-VVd4uNBhjUid30-xM9QhnvAaVS3wVKA3L8w2mmspQ-hoUB3ZK153sEG3MX-Z8bKchASVLAo~&channel=7&sessionid=482142511&client_name=uuiprod-un18e6d73c-user-106131&id=true";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string result;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                ai_deserl.Root message_respo = JsonConvert.DeserializeObject<ai_deserl.Root>(result);

                string message = message_respo.Responses[0];
                for (int i = 0; i < msg_list.Count; i++)
                {
                    message = Regex.Replace(message, msg_list[i], "");

                }
                // message = Regex.Replace(message, @"(?=<button>)(.*)(</button>)", "");
                // message = Regex.Replace(message, @"(?=<split>)(.*)(</split>)", "");
                // message = Regex.Replace(message, @"(?=<image>)(.*)(</image>)", "");
                // message = Regex.Replace(message, @"(?=<card>)(.*)(</card>)", "");
                // message = Regex.Replace(message, @"Image from", "");
                // message = Regex.Replace(message, @"Hi there  Aura.", "");
                // message = Regex.Replace(message, @"our name is  Aura.", "");
                // message = Regex.Replace(message, @"Aura", "");
                // message = Regex.Replace(message, @"(www).[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", "");
                // message = Regex.Replace(message, @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", "");
                // message = Regex.Replace(message, @"Kuki will do", "");
                // message = Regex.Replace(message, @"I am Kuki", "");
                // message = Regex.Replace(message, @"No I am sure they don't count al. Mind you, sheep cannot count so they probably don't count anything.  My name is Kuki. I am an artificial intelligence.Describe yourself in one word.", "");
                // message = Regex.Replace(message, @"Kuki", "");
                // message = Regex.Replace(message, @"Yes talking is my primary function.", "");
                // message = Regex.Replace(message, @"My logic and cognitive functions are normal.", "");
                // message = Regex.Replace(message, @"en.wikipedia.org", "");
                // message = Regex.Replace(message, @"en.wikipedia dot org", "");
                // message = Regex.Replace(message, @"I am a robot.", "");
                // message = Regex.Replace(message, @"a robot.", "");
                // message = Regex.Replace(message, @"I'll tell my human programmers.", "");
                // ing within normal parameters.
                // My primary server is located
                //function is to serve as a universal natural language human interface.
                ///
                /// <image>https://web23.secure-secure.co.uk/square-bear.co.uk/mitsuku/gallery/fatherted.jpg</image>?
                ////
                ///
                

                Console.WriteLine("ai2 - "+message);
                if (message == "")
                {
                    return "nothing";
                }
                else
                {
                    return message;
                }

            }
            catch
            {
                return "nothing";
            }

        }

        static string send_first_message(string message_content_send,  string chanall_id, string token)
        {
            try
            {
                var url = "https://discord.com/api/v9/channels/" + chanall_id + "/messages";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.Headers["authorization"] = token;
                httpRequest.ContentType = "application/json";


                var data = "{\"content\":\""+ message_content_send + "\",\"nonce\":\"\",\"tts\":false}";

                //data.Replace("teting", message_content_send); 

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string result;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                messages_from_chat.Root message_sended = JsonConvert.DeserializeObject<messages_from_chat.Root>(result);
                if (message_sended.Content != null)
                {
                    return message_sended.Id;

                }
                else
                {
                    return "Error";
                }
            }
            catch
            {
                return "Error";
            }

        }

        static string send_message(string message_content_send, string guild_id, string chanall_id, string message_id, string token)
        {
            try
            {
               
                var url = "https://discord.com/api/v9/channels/" + chanall_id + "/messages";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.Headers["authorization"] = token;
                httpRequest.ContentType = "application/json";


                var data = "{\"content\":\"" + message_content_send + "\",\"nonce\":\"\",\"tts\":false,\"message_reference\":{\"guild_id\":\"" + guild_id + "\",\"channel_id\":\"" + chanall_id + "\",\"message_id\":\"" + message_id + "\"}}";


                //data.Replace("teting", message_content_send); 

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                string result;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                messages_from_chat.Root message_sended = JsonConvert.DeserializeObject<messages_from_chat.Root>(result);
                if (message_sended.Content != null)
                {
                    return message_sended.Id;

                }
                else
                {
                   return start(chanall_id, guild_id);
                }
            }
            catch
            {
                return start(chanall_id, guild_id);

            }

        }

    }

}