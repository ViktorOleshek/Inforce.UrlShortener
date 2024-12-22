import { BaseModel } from './base-model';

export class UrlModel extends BaseModel {
  originalUrl: string;
  shortUrl: string;
  createdDate: Date;
  creatorId: number;
  createdBy: string;

  constructor(
    id: number = 0,
    originalUrl: string = '',
    shortUrl: string = '',
    createdDate: Date = new Date(),
    creatorId: number = 0,
    createdBy: string = ''
  ) {
    super(id);
    this.originalUrl = originalUrl;
    this.shortUrl = shortUrl;
    this.createdDate = createdDate;
    this.creatorId = creatorId;
    this.createdBy = createdBy;
  }
}
