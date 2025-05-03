export interface LogEntry {
    id?: number;
    date: Date;   
    logLevel: string;
    message: string;
    exception: string;
    source: string;
  }
  