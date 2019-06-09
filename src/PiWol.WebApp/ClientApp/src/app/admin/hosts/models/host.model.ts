export class HostModel {
  id: string;
  macAddress: string;
  ipAddress: string;
  netmask: string;
  hostname: string;
  status: HostStatus;
}

export enum HostStatus {
  unknown = 0,
  online = 1,
  offline = 2
}

