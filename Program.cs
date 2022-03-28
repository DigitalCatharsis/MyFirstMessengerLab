using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;    //for stringbuilder
using System.Threading.Tasks;

namespace MyFirstMessengerLab
{

    //    прога должна быть залита на гитхаб
    //   Реализовать класс Message, которое должно имет свойства:
    //    - отправитель
    //    - дата отправки

    //На основе этого класса нужно реализовать следующие классы:
    //    - TextMessage:
    //        - дополнительно имеет свойство текст
    //    - ImageMessage:
    //        - должно дополнять TextMessage
    //        - дополнительно имеет свойство url изображения
    //    - VoiceMessage:
    //        - должно иметь свойство url аудиосообщения

    //Так же каждое из этих сообщений должно иметь метод для сериализации сообщения в StringBuilder:
    //     - в данном случае достаточно добавить строки в формате "свойство=значение" в StringBuilder

    //Требования:
    //    - все классы являются потомками Message
    //    - нужно реализовать функцию, которая принимая массив типа Message, сериализовала бы их в консоль
    //    - соблюдать DRY, если видно, что блок кода повторяется, значит что-то идет не так
    //    - соблюдать при этом принцип подстановки Лисков


    //        сообщения генеришь руками в программе
    //рандомно или заранее заготовленный массив - не важно

    //         в массиве должно быть, как минимум, по одному экземпляру классов TextMessage, ImageMessage, VoiceMessage

    //Уточню задачу
    //Каждый класс сообщения должен иметь метод, который принимает stringbuilder в который оно себя сериализует

    //    В методе, который выводит массив сообщений, создаётся единственный экземпляр stringbuilder который передаётся во все сообщения по очереди

    //    И уже потом он выводится в консоль





    //public delegate StringBuilder NoParams();

    abstract public class Message
    {


        public Message(string sender, DateTime sentDate = default)
        {
            Sender = sender;
            this.SentDate = sentDate;
        }

        public string Sender { get; set; }

        public DateTime SentDate { get; set; }

        abstract protected void InternalSerialz(StringBuilder outputSB);
        public void MessageSerialization(StringBuilder OutputSB)
        {
            OutputSB.AppendLine(Sender);      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            OutputSB.AppendLine(SentDate.ToString());      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            OutputSB.AppendLine();      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            InternalSerialz(OutputSB);
        }

    }



    public class TextMessege : Message   //Получилось нагромождение иницилизации текста...
    {

        public string Text { get; set; }

        public TextMessege(string sender) : base(sender)
        {
            Text = "";
        }

        public TextMessege(string sender, string textContent) : base(sender)
        {
            Text = textContent;
        }

        protected override void InternalSerialz(StringBuilder OutputSB)
        {
            OutputSB.AppendLine(Text);      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            OutputSB.AppendLine();      //{ Sender, SentDate, "\r\n", Text, "\r\n" };            
        }



    }

    public class ImageMessage : TextMessege
    {
        public ImageMessage(string sender, string imageUrl) : base(sender)   //А можно брать значение text из конструктора textmessage, если ничего не указал?
        {
            ImageUrl = imageUrl;
            if (imageUrl == null)
            {
                throw new Exception("No url");
            }
        }


        public string ImageUrl { get; set ; }

        protected override void InternalSerialz(StringBuilder outputSB)
        {
            base.InternalSerialz(outputSB);
            outputSB.AppendLine(ImageUrl);      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            outputSB.AppendLine();      //{ Sender, SentDate, "\r\n", Text, "\r\n" };            
            outputSB.AppendLine(new string('=', 50));
        }

    }

    public class VoiceMessage : Message
    {
        public VoiceMessage(string sender) : base(sender)
        {
            if (VoiceMesURL == null)
            {
                throw new Exception("No url");
            }
        }
        public VoiceMessage(string sender, string imageUrl) : base(sender)   //А можно брать значение text из конструктора textmessage, если ничего не указал?
        {
            VoiceMesURL = imageUrl;
        }



        public string VoiceMesURL { get; set; }

        protected override void InternalSerialz(StringBuilder outputSB)
        {
            outputSB.AppendLine(VoiceMesURL);      //{ Sender, SentDate, "\r\n", Text, "\r\n" };
            outputSB.AppendLine();      //{ Sender, SentDate, "\r\n", Text, "\r\n" };            
            outputSB.AppendLine(new string('=', 50));
        }
    }



    internal class Program
    {

        static void Main(string[] args)
        {
            var MessagesArray = new Message[11];
            MessagesArray[1] = (new ImageMessage(sender: "ImageDominator2000@mail.com", imageUrl: @"http://mokriekiski_iz_priuta_besplatno_onlain_bezSMS_bezRegistatsii_kotiki.com/big_and_wet/fluffy_kitty.png"));
            MessagesArray[3] = (new TextMessege(sender: "TextDominator4000@mail.com", textContent: @" Это текстовое сообщение"));
            MessagesArray[4] = (new TextMessege(sender: "AudioDominator5000@mail.com"));
            MessagesArray[5] = (new VoiceMessage(sender: "AudioDominator6000@mail.com", @"http://mokriekiski_iz_priuta_besplatno_onlain_bezSMS_bezRegistatsii_kotiki.com/big_and_wet/fluffy_kitty.mp3"));

            PrintMessages(MessagesArray);
        }

        static void PrintMessages(Message[] messages)
        {
            var sb = new StringBuilder();

            foreach (var message in messages)
            {
               if (message != null)
                {
                    message.MessageSerialization(OutputSB: sb);
                }               
            }
            Console.Write(sb.ToString());
        }
    }



}