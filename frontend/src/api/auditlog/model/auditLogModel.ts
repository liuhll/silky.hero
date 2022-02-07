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

export interface GetAuditLogDetailModel {
  id: number;
  userName: string;
  realName: string;
  surname: string;
  birthDay: Date;
  sex: number;
  email: string;
  telPhone: string;
  mobilePhone: string;
  jobNumber: string;
  createdTime: Date;
  actions: GetAuditLogActionModel[];
}

export interface GetAuditLogActionModel {
  id: number;
  hostName: string;
  serviceEntryId: string;
  hostAddress: string;
  serviceName: string;
  serviceKey: string;
  methodName: string;
  parameters: string;
  isDistributedTransaction: boolean;
  executionTime: Date;
  executionDuration: number;
  exceptionMessage: string;
}
