export interface IPrediction {
    Id: bigint;
    StudentId: string;
    MatchId: bigint;
    LocalNationalTeamPredictedGoals: number;
    VisitorNationalTeamPredictedGoals: number;
}