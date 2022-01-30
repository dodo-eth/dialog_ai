using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; 

namespace dialog_ai
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    class messages_from_chat
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Author
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("avatar")]
            public string Avatar { get; set; }

            [JsonProperty("discriminator")]
            public string Discriminator { get; set; }

            [JsonProperty("public_flags")]
            public int PublicFlags { get; set; }
        }

        public class Mention
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("avatar")]
            public string Avatar { get; set; }

            [JsonProperty("discriminator")]
            public string Discriminator { get; set; }

            [JsonProperty("public_flags")]
            public int PublicFlags { get; set; }
        }

        public class MessageReference
        {
            [JsonProperty("channel_id")]
            public string ChannelId { get; set; }

            [JsonProperty("guild_id")]
            public string GuildId { get; set; }

            [JsonProperty("message_id")]
            public string MessageId { get; set; }
        }

        public class ReferencedMessage
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }

            [JsonProperty("channel_id")]
            public string ChannelId { get; set; }

            [JsonProperty("author")]
            public Author Author { get; set; }

            [JsonProperty("attachments")]
            public List<object> Attachments { get; set; }

            [JsonProperty("embeds")]
            public List<object> Embeds { get; set; }

            [JsonProperty("mentions")]
            public List<object> Mentions { get; set; }

            [JsonProperty("mention_roles")]
            public List<object> MentionRoles { get; set; }

            [JsonProperty("pinned")]
            public bool Pinned { get; set; }

            [JsonProperty("mention_everyone")]
            public bool MentionEveryone { get; set; }

            [JsonProperty("tts")]
            public bool Tts { get; set; }

            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }

            [JsonProperty("edited_timestamp")]
            public object EditedTimestamp { get; set; }

            [JsonProperty("flags")]
            public int Flags { get; set; }

            [JsonProperty("components")]
            public List<object> Components { get; set; }
        }

        public class Root
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }

            [JsonProperty("channel_id")]
            public string ChannelId { get; set; }

            [JsonProperty("author")]
            public Author Author { get; set; }

            [JsonProperty("attachments")]
            public List<object> Attachments { get; set; }

            [JsonProperty("embeds")]
            public List<object> Embeds { get; set; }

            [JsonProperty("mentions")]
            public List<Mention> Mentions { get; set; }

            [JsonProperty("mention_roles")]
            public List<object> MentionRoles { get; set; }

            [JsonProperty("pinned")]
            public bool Pinned { get; set; }

            [JsonProperty("mention_everyone")]
            public bool MentionEveryone { get; set; }

            [JsonProperty("tts")]
            public bool Tts { get; set; }

            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }

            [JsonProperty("edited_timestamp")]
            public object EditedTimestamp { get; set; }

            [JsonProperty("flags")]
            public int Flags { get; set; }

            [JsonProperty("components")]
            public List<object> Components { get; set; }

            [JsonProperty("message_reference")]
            public MessageReference MessageReference { get; set; }

            [JsonProperty("referenced_message")]
            public ReferencedMessage ReferencedMessage { get; set; }
        }




    }

}
