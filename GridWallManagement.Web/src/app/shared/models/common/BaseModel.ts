export class BaseModel {
  id: string;
  createdBy: string;
  createdDate: Date;
  modifiedBy?: string;
  modifiedDate?: Date;

  constructor(
    id: string = '',
    createdBy: string = '',
    createdDate: Date = new Date(),
    modifiedBy?: string,
    modifiedDate?: Date
  ) {
    this.id = id;
    this.createdBy = createdBy;
    this.createdDate = createdDate;
    this.modifiedBy = modifiedBy;
    this.modifiedDate = modifiedDate;
  }
}
