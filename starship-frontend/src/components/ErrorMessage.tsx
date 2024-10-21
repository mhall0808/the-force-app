import React from 'react';

interface ErrorMessageProps {
  message: string;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => (
  <div
    className="alert alert-danger position-fixed bottom-0 start-50 translate-middle-x mb-3"
    role="alert"
  >
    {message}
  </div>
);

export default ErrorMessage;
