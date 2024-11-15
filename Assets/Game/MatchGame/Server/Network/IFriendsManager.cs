 // This asset was uploaded by https://unityassetcollection.com

using System;
using System.Collections.Generic;

namespace POPBlocks.Server.Network
{
	public interface IFriendsManager {
		void GetFriends (Action<Dictionary<string,string>> Callback) ;

		void PlaceFriendsPositionsOnMap (Action<Dictionary<string,int>> Callback);

		void GetLeadboardOnLevel (int LevelNumber, Action<List<LeadboardPlayerData>> Callback);

		void Logout ();
	}
}


