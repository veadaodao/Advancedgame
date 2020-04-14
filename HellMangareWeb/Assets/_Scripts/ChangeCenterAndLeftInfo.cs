using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCenterAndLeftInfo : MonoBehaviour
{
    public WholeBuildInfoManager wholeBuildInfoManager;
    public int BuildingLevelInfo;


    void Update()
    {
        switch (BuildingLevelInfo)
        {
            case 0:
                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(false);

                //8
                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(false);
                break;
            case 1:
                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(false);


                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(false);
                break;
            case 2:
                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(false);


                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(false);
                break;
            case 3:
                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(false);


                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(false);
                break;
            case 4:
                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(false);


                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(false);
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                wholeBuildInfoManager.DeathInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftDeathPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.FireHeatingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftFireFountainPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.SnowCoolingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftSnowCampPanel.gameObject.SetActive(false);
                break;
        }

    }
}
