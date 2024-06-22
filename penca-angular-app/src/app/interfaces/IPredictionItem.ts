export interface IPredictionItem {
    predictionId: bigint;
    matchId: bigint;
    localNationalTeam: string;
    localNationalTeamPredictedGoals: number;
    visitorNationalTeam: string;
    visitorNationalTeamPredictedGoals: number;
    date: Date;
    stadiumName: string;
    state: string;
    city: string;
}