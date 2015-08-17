﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace Grabacr07.KanColleWrapper.Models
{
	[DebuggerDisplay("[{Id}] {Title} - {Detail}")]
	public class Mission : RawDataWrapper<kcsapi_mission>, IIdentifiable
	{
		public int Id { get; private set; }

		public string Title { get; private set; }

		public string TitleUntranslated { get; private set; }

		public string Detail { get; private set; }

		public string DetailUntranslated { get; private set; }

		public Mission(kcsapi_mission mission)
			: base(mission)
		{
			this.Id = mission.api_id;
            this.Title = KanColleClient.Current.Translations.GetTranslation(mission.api_name, TranslationType.ExpeditionTitle, mission, mission.api_id);
			this.TitleUntranslated = mission.api_name;
            this.Detail = KanColleClient.Current.Translations.GetTranslation(mission.api_details, TranslationType.ExpeditionDetail, mission, mission.api_id);
			this.DetailUntranslated = mission.api_details;
		}

		public void Update()
		{
			var mission = this.RawData as kcsapi_mission;
			this.Title = KanColleClient.Current.Translations.GetTranslation(mission.api_name, TranslationType.ExpeditionTitle, mission, mission.api_id);
			this.TitleUntranslated = mission.api_name;
			this.Detail = KanColleClient.Current.Translations.GetTranslation(mission.api_details, TranslationType.ExpeditionDetail, mission, mission.api_id);
			this.DetailUntranslated = mission.api_details;
		}
	}
}
