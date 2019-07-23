# Data models

```
WorkOrderTemplate {
  Id,
  ProjectId,
  Name,
  IsAdHoc: bool,
  CompanyId,
}
```

```
WorkOrder {
  Id,
  ProjectId,
  TemplateId,
  IsPreviewOnly: bool,
  StartDate: DateTime,
  FinishedDate: DateTime?,
}
``` 

```
TemplateDoor {
  TemplateId,

  Door: Door,
}
```

```
WorkOrderDoor {
  WorkOrderId,
  FinishedDate: DateTime?,

  Door: Door,
}
```

```
Door {
	H3: Hardware,
	...
}
```

```
Hardware {
	string Id,
	int? Qty,
	string Content,
	string Surf,

	bool IsMaintainable,
	bool IsMaintained,
	string ChecklistId 
}
```

Collection items are created to signify they are `IsMaintainable`:

```
TemplateHardwareCollection {
  Id,
  ProjectId,
  TemplateId,

  FieldName,
  Content,
  
  ChecklistId
}
```

## Creation 

1. Save `WorkOrderTemplate`
2. Save `TemplateHardwareCollection`
3. Map `IsMaintainable` and `ChecklistId` to `TemplateDoors`
4. Save `TemplateDoors`
5. Save `WorkOrder` instances

## Update

1. Update `WorkOrderTemplate`
2. Remove and save all `TemplateHardwareCollection`
3. Map `IsMaintainable` and `ChecklistId` to `TemplateDoors`
4. Remove and save all `TemplateDoors`
5. Save new `WorkOrder` instances
