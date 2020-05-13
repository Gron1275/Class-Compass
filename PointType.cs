namespace RecommendationEngine
{
    public enum PointType
    {
        noise = 0,
        border = 1, //Actually designate which points are border points the suggestions given to them from their cluster are weighted less than their interests 
                    //b/c they are less similar so their recommendations should be based more on the item based part of the algo than the user similarity
        core = 2
    };
}