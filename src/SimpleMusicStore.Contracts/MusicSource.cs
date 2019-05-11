using SimpleMusicStore.Models.MusicLibraries;
using System;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts
{
	public interface MusicSource
	{
		Task<RecordInfo> Record(Uri uri);
		Task<LabelInfo> Label(int id);
		Task<ArtistInfo> Artist(int id);

	}
}
