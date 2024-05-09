import React from "react";
import { Pagination } from "../../../../Contexts/Shared/Domain/Criteria/Pagination";

interface Props {
  pagination: Pagination
}

export const DevFooter : React.FC<Props> = ({pagination}) => {
  return (
    <div className="dev-footer">
      Page {pagination.page} of {pagination.totalPage}
    </div>
  )
}