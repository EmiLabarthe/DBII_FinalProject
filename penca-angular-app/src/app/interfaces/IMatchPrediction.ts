export interface IMatchPrediction {
    Id: bigint;
    StudentId: string;
    MatchId: bigint;
    LocalNationalTeamPredictedGoals: number;
    VisitorNationalTeamPredictedGoals: number;
}