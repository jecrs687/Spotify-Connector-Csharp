namespace Spotify_Connect.Business.Configuration

{
	public class Topics
	{
		public string Artist { get; set; }
	}
	public class KafkaConfiguration
	{
		public  string BootstrapServers { get; set; }
		public  string GroupId { get; set; }
		public  Topics Topics { get; set; }
	}
}
