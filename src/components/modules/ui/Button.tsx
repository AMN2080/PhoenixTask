interface ButtonProps {
  children: React.ReactNode;
  className?: string;
}

export default function Button({ children, className }: ButtonProps) {
  return (
    <button className={`btn btn-primary font-yekan-heading ${className}`}>
      {children}
    </button>
  );
}
