import { HostsModule } from './hosts.module';

describe('HostsModule', () => {
  let hostsModule: HostsModule;

  beforeEach(() => {
    hostsModule = new HostsModule();
  });

  it('should create an instance', () => {
    expect(hostsModule).toBeTruthy();
  });
});
