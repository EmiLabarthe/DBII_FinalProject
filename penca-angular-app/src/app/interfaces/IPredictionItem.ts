export interface IPredictionItem {
    PredictionId: string;
    LocalNationalTeam: string;
    LocalNationalTeamPredictedGoals: number;
    VisitorNationalTeam: string;
    VisitorNationalTeamPredictedGoals: number;
    Date: Date;
    StadiumName: string;
    State: string;
    City: string;
}