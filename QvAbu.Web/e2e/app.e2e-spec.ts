import { QvAbu.WebPage } from './app.po';

describe('qv-abu.web App', () => {
  let page: QvAbu.WebPage;

  beforeEach(() => {
    page = new QvAbu.WebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
