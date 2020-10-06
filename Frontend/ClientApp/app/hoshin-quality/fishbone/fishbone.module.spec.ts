import { FishboneModule } from './fishbone.module';

describe('FishboneModule', () => {
  let fishboneModule: FishboneModule;

  beforeEach(() => {
    fishboneModule = new FishboneModule();
  });

  it('should create an instance', () => {
    expect(fishboneModule).toBeTruthy();
  });
});
