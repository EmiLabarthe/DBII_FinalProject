export interface IPredictionResultItem {
    localNationalTeam: string;
    localNationalTeamGoals: number;
    localNationalTeamPredictedGoals: number;
    visitorNationalTeam: string;
    visitorNationalTeamGoals: number;
    visitorNationalTeamPredictedGoals: number;
    points: number;
    date: Date;
    stadiumName: string;
    state: string;
    city: string;
}