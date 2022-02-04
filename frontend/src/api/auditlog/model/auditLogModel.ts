export interface GetAuditLogPageModel {
  id: number;
  userName: string;
  executionTime: Date;
  executionDuration: number;
  url: string;
  httpMethod: string;
  clientIpAddress: string;
  httpStatusCode: number;
}
