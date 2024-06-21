export interface IPredictionItem {
    predictionId: string;
    localNationalTeam: string;
    localNationalTeamPredictedGoals: number;
    visitorNationalTeam: string;
    visitorNationalTeamPredictedGoals: number;
    date: Date;
    stadiumName: string;
    state: string;
    city: string;
}