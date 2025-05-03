export interface AlertMessage {
  type: 'success' | 'danger' | 'warning' | 'info';
  text: string;
}
